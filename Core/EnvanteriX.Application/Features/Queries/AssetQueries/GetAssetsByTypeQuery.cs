using EnvanteriX.Application.Features.Results.AssetResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetQueries
{
  public  class GetAssetsByTypeQuery :IRequest<List<GetAssetsByTypeQueryResult>>
    {
        public int AssetTypeId { get; set; }
        public GetAssetsByTypeQuery(int assetTypeId)
        {
            AssetTypeId = assetTypeId;
        }
    }
}
