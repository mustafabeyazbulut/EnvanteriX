namespace EnvanteriX.Application.Features.Results.VendorResults
{
    public class GetVendorByIdQueryResult
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
