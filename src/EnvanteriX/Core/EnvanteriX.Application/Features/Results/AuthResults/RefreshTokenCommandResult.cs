namespace EnvanteriX.Application.Features.Results.AuthResults
{
    public class RefreshTokenCommandResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
