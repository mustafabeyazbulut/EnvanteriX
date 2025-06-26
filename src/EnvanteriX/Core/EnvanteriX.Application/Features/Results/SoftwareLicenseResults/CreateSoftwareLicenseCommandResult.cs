using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Results.SoftwareLicenseResults
{
    public class CreateSoftwareLicenseCommandResult
    {
        public int SoftwareLicenseId { get; set; }

        public CreateSoftwareLicenseCommandResult(SoftwareLicense license)
        {
            SoftwareLicenseId = license.Id;
        }
    }
}
