using MediatR;
using EnvanteriX.Application.Features.Results.SoftwareLicenseResults;

namespace EnvanteriX.Application.Features.Commands.SoftwareLicenseCommands
{
    public class UpdateSoftwareLicenseCommand : IRequest<UpdateSoftwareLicenseCommandResult>
    {
        public int SoftwareLicenseId { get; set; }
        public int AssetId { get; set; }
        public string LicenseKey { get; set; }
        public string SoftwareName { get; set; }
        public string Version { get; set; }
        public int VendorId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? AssignedUserId { get; set; }
    }
}
