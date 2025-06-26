using EnvanteriX.Application.Features.Results.AssetMovementResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetMovementQueries
{
    public class GetAssetMovementByIdQuery : IRequest<GetAssetMovementByIdQueryResult>
    {
        public int Id { get; set; }

        public GetAssetMovementByIdQuery(int id)
        {
            Id = id;
        }
    }
}
