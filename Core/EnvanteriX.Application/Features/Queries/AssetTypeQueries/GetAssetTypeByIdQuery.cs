using EnvanteriX.Application.Features.Results.AssetTypeResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetTypeQueries
{
    public class GetAssetTypeByIdQuery : IRequest<GetAssetTypeByIdQueryResult>
    {
        public int Id { get; set; }

        public GetAssetTypeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
