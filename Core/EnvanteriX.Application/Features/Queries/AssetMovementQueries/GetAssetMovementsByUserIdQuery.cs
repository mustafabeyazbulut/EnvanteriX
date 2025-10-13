using EnvanteriX.Application.Features.Results.AssetMovementResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetMovementQueries
{
    public class GetAssetMovementsByUserIdQuery : IRequest<List<GetAssetMovementsByUserIdQueryResult>>
    {
        public int UserId { get; set; }

        public GetAssetMovementsByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
