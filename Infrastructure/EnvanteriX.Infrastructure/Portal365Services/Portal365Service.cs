using EnvanteriX.Application.Interfaces.Portal365Interfaces;
using EnvanteriX.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using System.DirectoryServices;
using System.Reflection.PortableExecutable;
using System.Text;

namespace EnvanteriX.Infrastructure.Portal365Services
{
    public class Portal365Service : IPortal365Service
    {
        private readonly HttpClient _httpClient;
        private readonly Portal365Settings _settings;

        public Portal365Service(HttpClient httpClient, IOptions<Portal365Settings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<List<dynamic>> GetAllUsersAsync(Portal365 portalConfig)
        {
            try
            {
                var accessToken = await GetAccessTokenAsync(portalConfig);
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Access token alınamadı");
                }

                var PortalUsers = await GetNewLicensedUsersAsync(accessToken, portalConfig);
                return PortalUsers.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"GetAllPortalUsersAsync hatası: {ex.Message}", ex);
            }
        }

        public async Task<string> GetAccessTokenAsync(Portal365 portalConfig)
        {
            try
            {
                var requestBody = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", portalConfig.ClientId),
                    new KeyValuePair<string, string>("client_secret", portalConfig.ClientSecret),
                    new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                var requestContent = new FormUrlEncodedContent(requestBody);
                var response = await _httpClient.PostAsync(
                    $"https://login.microsoftonline.com/{portalConfig.TenantId}/oauth2/v2.0/token",
                    requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<JObject>(responseContent);
                    return tokenResponse["access_token"]?.ToString();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Token alma hatası: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"GetAccessTokenAsync hatası: {ex.Message}", ex);
            }
        }

        private async Task<List<PortalUser>> GetNewLicensedUsersAsync(string accessToken, Portal365 portalConfig)
        {
            var PortalUsers = new List<PortalUser>();

            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                _httpClient.DefaultRequestHeaders.Add("ConsistencyLevel", "eventual");

                string url = "https://graph.microsoft.com/v1.0/users?$select=id,displayName,mail,userPrincipalName,assignedLicenses,createdDateTime,onPremisesDistinguishedName,mobilePhone,companyName,department&$top=999";


                do
                {
                    var response = await _httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Kullanıcı alma hatası: {response.StatusCode} - {errorContent}");
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var PortalUsersResponse = JsonConvert.DeserializeObject<JObject>(responseContent);
                    var PortalUserArray = PortalUsersResponse["value"] as JArray;

                    foreach (var PortalUserItem in PortalUserArray)
                    {
                        // Lisanslı mı kontrol et
                        var assignedLicenses = PortalUserItem["assignedLicenses"] as JArray;
                        if (assignedLicenses == null || assignedLicenses.Count == 0)
                            continue;

                        // OU çek
                        string ou = null;
                        var distinguishedName = PortalUserItem["onPremisesDistinguishedName"]?.ToString();
                        if (!string.IsNullOrEmpty(distinguishedName))
                        {
                            var parts = distinguishedName.Split(',');
                            var ouPart = parts.FirstOrDefault(p => p.StartsWith("OU=", StringComparison.OrdinalIgnoreCase));
                            if (ouPart != null)
                                ou = ouPart.Substring(3);
                        }
                        //PortalUserItem["PortalUserPrincipalName"]?.ToString()
                        var PortalUser = new PortalUser
                        {
                            Id = PortalUserItem["id"]?.ToString(),
                            DisplayName = PortalUserItem["displayName"]?.ToString(),
                            Mail = PortalUserItem["mail"]?.ToString(),
                            UserPrincipalName = PortalUserItem["userPrincipalName"]?.ToString(),
                            OU = ou,
                            CompanyName = PortalUserItem["companyName"]?.ToString(),
                            PhoneNumber = PortalUserItem["mobilePhone"]?.ToString(),
                            Department = PortalUserItem["department"]?.ToString(),
                        };

                        // Mail geçerliliği kontrolü
                        if (string.IsNullOrEmpty(PortalUser.Mail)
                            || !PortalUser.Mail.Contains("@")
                            || (!PortalUser.Mail.EndsWith("isri.com.tr", StringComparison.OrdinalIgnoreCase)
                                && !PortalUser.Mail.EndsWith("aundeteknik.com", StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        //// CreatedDateTime kontrolü
                        //if (DateTime.TryParse(PortalUserItem["createdDateTime"]?.ToString(), null,
                        //    System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime createdDate))
                        //{
                        //    PortalUser.CreatedDateTime = createdDate;
                        //    var date = DateTime.Now.AddDays(-1).ToUniversalTime(); // Son 1 gün
                        //    if (createdDate <= date)
                        //        continue;
                        //}
                        //else
                        //{
                        //    continue;
                        //}

                        // OU kontrolü
                        if (IsPortalUserInTargetOU(PortalUser))
                        {
                            PortalUsers.Add(PortalUser);
                        }
                    }

                    // Sonraki sayfa varsa URL güncelle
                    url = PortalUsersResponse["@odata.nextLink"]?.ToString();

                } while (!string.IsNullOrEmpty(url));
            }
            catch (Exception ex)
            {
                throw new Exception($"GetNewLicensedPortalUsersAsync hatası: {ex.Message}", ex);
            }

            return PortalUsers;
        }

        private bool IsPortalUserInTargetOU(PortalUser PortalUser)
        {
            if (string.IsNullOrEmpty(PortalUser.OU))
                return false;

            var targetOUs = _settings.TargetOUs;
            return targetOUs.Any(ou => PortalUser.OU.Contains(ou, StringComparison.OrdinalIgnoreCase));
        }

        public async Task SendEmailAsync(Portal365 model, string toMail, string subject, string emailBody)
        {
            try
            {
                var accessToken = await GetAccessTokenAsync(model);
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Access token alınamadı");
                }

                string _senderEmail = "aundesmtp@aundeteknik.com";
                var emailMessage = new
                {
                    message = new
                    {
                        subject = subject,
                        body = new
                        {
                            contentType = "HTML",
                            content = emailBody
                        },
                        toRecipients = new[]
                        {
                    new
                    {
                        emailAddress = new
                        {
                            address = toMail,
                            name = toMail
                        }
                    }
                }
                    },
                    saveToSentItems = true
                };

                var json = JsonConvert.SerializeObject(emailMessage);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await _httpClient.PostAsync($"https://graph.microsoft.com/v1.0/users/{_senderEmail}/sendMail", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Mail gönderme hatası: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Mail gönderme hatası: {ex.Message}");
            }
        }

        public async Task<bool> LoginAsync( string username, string password)
        {
            string ldapPath = "LDAP://192.168.7.2"; // Active Directory'nin LDAP yolu
            string domain = "aundeteknik.com"; // Domain adı
            username = username.Substring(0, username.IndexOf("@"));
            try
            {
                // Kullanıcı kimlik doğrulama
                System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(ldapPath, $"{domain}\\{username}", password);
                object obj = entry.NativeObject; // Bağlantı kurma işlemi

                // Kullanıcı bilgilerini alma
                DirectorySearcher searcher = new DirectorySearcher(entry)
                {
                    Filter = $"(samAccountName={username})" // Kullanıcı adı filtreleme
                };
                searcher.PropertiesToLoad.Add("title"); // Unvan bilgisini yükle
                searcher.PropertiesToLoad.Add("displayName"); // Görünen adı yükle (isteğe bağlı)

                SearchResult result = searcher.FindOne();

                if (result != null)
                {
                    if (result.Properties.Contains("title"))
                    {
                        //userTitle = result.Properties["title"][0].ToString(); // Kullanıcının unvanı
                    }
                }

                return true; // Başarılı giriş
            }
            catch (Exception)
            {
                return false; // Hatalı giriş
            }
        }



        public class PortalUser
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public string Mail { get; set; }
            public string UserPrincipalName { get; set; }
            public string Department { get; set; }
            public string CompanyName { get; set; }
            public string PhoneNumber { get; set; }
            public string OU { get; set; }
            public DateTime CreatedDateTime { get; set; }
        }
    }
}