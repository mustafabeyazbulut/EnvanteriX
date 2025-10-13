using EnvanteriX.Application.Features.Results.AssetResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetQueries
{
    public class GetAllAssetsQuery :IRequest<List<GetAllAssetsQueryResult>>
    {
    }
}
