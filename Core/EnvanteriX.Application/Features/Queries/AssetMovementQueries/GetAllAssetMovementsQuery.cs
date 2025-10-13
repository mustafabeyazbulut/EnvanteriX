using EnvanteriX.Application.Features.Results.AssetMovementResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetMovementQueries
{
    public class GetAllAssetMovementsQuery : IRequest<List<GetAllAssetMovementsQueryResult>>
    {
    }
}
