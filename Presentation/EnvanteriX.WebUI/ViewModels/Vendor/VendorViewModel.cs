namespace EnvanteriX.WebUI.ViewModels.Vendor
{
    public class VendorViewModel
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
