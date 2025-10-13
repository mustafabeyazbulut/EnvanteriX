using EnvanteriX.Application.Features.Results.AssetResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetQueries
{
   public class GetAssetByIdQuery : IRequest<GetAssetByIdQueryResult>
    {
        public int AssetId { get; set; }
        public GetAssetByIdQuery(int assetId)
        {
            AssetId = assetId;
        }
    }
}
