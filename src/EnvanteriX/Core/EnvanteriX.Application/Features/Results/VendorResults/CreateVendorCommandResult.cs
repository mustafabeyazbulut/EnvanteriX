using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Results.VendorResults
{
    public class CreateVendorCommandResult
    {
        public int VendorId { get; set; }

        public CreateVendorCommandResult(Vendor vendor)
        {
            VendorId = vendor.Id;
        }
    }
}
