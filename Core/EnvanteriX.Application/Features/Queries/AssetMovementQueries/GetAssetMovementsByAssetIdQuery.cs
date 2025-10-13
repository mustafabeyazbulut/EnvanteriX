using EnvanteriX.Application.Features.Results.AssetMovementResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetMovementQueries
{
    public class GetAssetMovementsByAssetIdQuery : IRequest<List<GetAssetMovementsByAssetIdQueryResult>>
    {
        public int AssetId { get; set; }

        public GetAssetMovementsByAssetIdQuery(int assetId)
        {
            AssetId = assetId;
        }
    }
}
