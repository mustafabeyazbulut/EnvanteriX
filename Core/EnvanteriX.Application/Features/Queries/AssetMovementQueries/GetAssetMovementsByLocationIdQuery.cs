using EnvanteriX.Application.Features.Results.AssetMovementResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetMovementQueries
{
    public class GetAssetMovementsByLocationIdQuery : IRequest<List<GetAssetMovementsByLocationIdQueryResult>>
    {
        public int LocationId { get; set; }

        public GetAssetMovementsByLocationIdQuery(int locationId)
        {
            LocationId = locationId;
        }
    }
}
