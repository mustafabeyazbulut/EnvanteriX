using MediatR;
using EnvanteriX.Application.Features.Results.AssetTypeResults;

namespace EnvanteriX.Application.Features.Queries.AssetTypeQueries
{
    public class GetAllAssetTypesQuery : IRequest<List<GetAllAssetTypesQueryResult>>
    {
    }
}
