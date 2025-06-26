namespace EnvanteriX.WebUI.Models
{
    public class TokenResponse
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime expiration { get; set; }
    }
}
