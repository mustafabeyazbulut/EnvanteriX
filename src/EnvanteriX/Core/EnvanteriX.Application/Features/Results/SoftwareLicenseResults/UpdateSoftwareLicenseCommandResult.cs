using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Results.SoftwareLicenseResults
{
    public class UpdateSoftwareLicenseCommandResult
    {
        public int SoftwareLicenseId { get; set; }

        public UpdateSoftwareLicenseCommandResult(SoftwareLicense license)
        {
            SoftwareLicenseId = license.Id;
        }
    }
}
