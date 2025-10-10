using EnvanteriX.Application.Features.Results.AssetTypeResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetTypeQueries
{
    public class GetAllActiveAssetTypesQuery : IRequest<List<GetAllActiveAssetTypesQueryResult>>
    {
    }
}
