using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.Portal365Queries;
using EnvanteriX.Application.Features.Rules.Portal365Rules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.Email;
using EnvanteriX.Application.Interfaces.PasswordGenerator;
using EnvanteriX.Application.Interfaces.Portal365Interfaces;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnvanteriX.Application.Features.Handlers.Portal365Handlers
{
    public class SyncPortal365UsersQueryHandler : BaseHandler, IRequestHandler<SyncPortal365UsersQuery, Unit>
    {
        private readonly Portal365Rules _portal365Rules;
        private readonly IPortal365Service _portal365Service;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        public SyncPortal365UsersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IPortal365Service portal365Service, Portal365Rules portal365Rules, UserManager<User> userManager, IPasswordGenerator passwordGenerator, IEmailTemplateProvider emailTemplateProvider) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Service = portal365Service;
            _portal365Rules = portal365Rules;
            _userManager = userManager;
            _passwordGenerator = passwordGenerator;
            _emailTemplateProvider = emailTemplateProvider;
        }

        public async Task<Unit> Handle(SyncPortal365UsersQuery request, CancellationToken cancellationToken)
        {
            var portal365Config = await _unitOfWork.GetReadRepository<Portal365>().GetAsync(
                predicate: x => !x.IsDeleted,
                orderBy: q => q.OrderByDescending(x => x.CreatedDate)
            );

            await _portal365Rules.Portal365ShouldExist(portal365Config);

            var portal365Users = await _portal365Service.GetAllUsersAsync(portal365Config);

            var distinctCompanies = portal365Users
                .Where(x => !string.IsNullOrWhiteSpace(x.CompanyName))
                .Select(x => x.CompanyName.Trim())
                .Distinct()
                .ToList();

            var distinctDepartments = portal365Users
                .Where(x => !string.IsNullOrWhiteSpace(x.Department))
                .Select(x => x.Department.Trim())
                .Distinct()
                .ToList();

            // 🔹 Kullanıcı Senkronizasyonu
            foreach (var user in portal365Users)
            {
                try
                {
                    string email = user.Mail;
                    if (string.IsNullOrEmpty(email))
                        continue;

                    var exists = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
                    if (exists != null)
                        continue;
                    if (email!= "iaydin@aundeteknik.com")
                    {
                        continue;
                    }

                    var newUser = new User
                    {
                        UserName = email.Split("@")[0],
                        Email = email,
                        NormalizedEmail = email.ToUpperInvariant(),
                        NormalizedUserName = email.Split("@")[0].ToUpperInvariant(),
                        FullName = user.DisplayName,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = user.PhoneNumber
                    };

                    var password = _passwordGenerator.Generate(8);
                    var resultUser = await _userManager.CreateAsync(newUser, password);

                    if (resultUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "user");
                        var emailTemplate = await _emailTemplateProvider.GetTemplateAsync("RegisterEmailTemplate");
                        if (!string.IsNullOrEmpty(emailTemplate))
                        {
                            // Placeholder’ları doldur
                            emailTemplate = emailTemplate.Replace("{{FullName}}", newUser.FullName)
                                                         .Replace("{{Url}}", "https://envanter.aundeteknik.com");

                            await _portal365Service.SendEmailAsync(portal365Config, newUser.Email,
                                "Aunde Envanter Kaydınız Tamamlandı", emailTemplate);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"❌ Kullanıcı oluşturulamadı: {email} -> {string.Join(", ", resultUser.Errors.Select(e => e.Description))}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Kullanıcı eklenirken hata oluştu: {user.Mail} -> {ex.Message}");
                    continue; // hata olsa bile sıradaki kullanıcıya geç
                }
            }

            // 🔹 Şirket Senkronizasyonu
            foreach (var company in distinctCompanies)
            {
                try
                {
                    string value = company; 
                    var exists = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Building == value);
                    if (exists != null)
                        continue;

                    var newLocation = new Location
                    {
                        Building = company,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        LastModifiedByEmail = _userEmail
                    };

                    await _unitOfWork.GetWriteRepository<Location>().AddAsync(newLocation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Şirket eklenirken hata oluştu: {company} -> {ex.Message}");
                    continue;
                }
            }

            // 🔹 Departman Senkronizasyonu
            foreach (var dept in distinctDepartments)
            {
                try
                {
                    string value = dept;
                    var exists = await _unitOfWork.GetReadRepository<Department>().GetAsync(x => x.Name == value);
                    if (exists != null)
                        continue;

                    var newDepartment = new Department
                    {
                        Name = dept,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        LastModifiedByEmail=_userEmail
                    };

                    await _unitOfWork.GetWriteRepository<Department>().AddAsync(newDepartment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Departman eklenirken hata oluştu: {dept} -> {ex.Message}");
                    continue;
                }
            }

            await _unitOfWork.SaveAsync(); // varsa

            Console.WriteLine("✅ Portal365 senkronizasyon işlemi tamamlandı.");

            return Unit.Value;
        }


        //public class PortalUser
        //{
        //    public string Id { get; set; }
        //    public string DisplayName { get; set; }
        //    public string Mail { get; set; }
        //    public string UserPrincipalName { get; set; }
        //    public string Department { get; set; }
        //    public string CompanyName { get; set; }
        //    public string PhoneNumber { get; set; }
        //    public string OU { get; set; }
        //    public DateTime CreatedDateTime { get; set; }
        //}
    }
}
