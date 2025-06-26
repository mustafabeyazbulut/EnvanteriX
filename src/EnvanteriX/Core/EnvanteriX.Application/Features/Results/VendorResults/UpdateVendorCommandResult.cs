using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Results.VendorResults
{
    public class UpdateVendorCommandResult
    {
        public int VendorId { get; set; }

        public UpdateVendorCommandResult(Vendor vendor)
        {
            VendorId = vendor.Id;
        }
    }
}
