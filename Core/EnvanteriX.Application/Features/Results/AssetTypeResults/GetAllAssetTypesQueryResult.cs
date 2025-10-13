
namespace EnvanteriX.Application.Features.Results.AssetTypeResults
{
    public class GetAllAssetTypesQueryResult
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
