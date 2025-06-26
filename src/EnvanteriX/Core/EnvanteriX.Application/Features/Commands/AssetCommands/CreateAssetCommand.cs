using EnvanteriX.Application.Features.Results.AssetResults;
using MediatR;

public class CreateAssetCommand : IRequest<CreateAssetCommandResult>
{
    public string AssetTag { get; set; }
    public string SerialNumber { get; set; }
    public int AssetTypeId { get; set; }
    public int ModelId { get; set; }
    public int VendorId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime WarrantyEndDate { get; set; }
    public decimal Cost { get; set; }
    public int LocationId { get; set; }
    public int? AssignedUserId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}
