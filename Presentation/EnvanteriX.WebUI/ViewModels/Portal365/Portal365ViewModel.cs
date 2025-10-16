namespace EnvanteriX.WebUI.ViewModels.Portal365
{
    public class Portal365ViewModel
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SenderEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
