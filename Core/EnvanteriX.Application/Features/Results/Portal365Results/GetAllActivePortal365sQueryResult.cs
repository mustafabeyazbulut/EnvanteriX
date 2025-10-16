namespace EnvanteriX.Application.Features.Results.Portal365Results
{
    public class GetAllActivePortal365sQueryResult
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SenderEmail { get; set; }
    }
}
