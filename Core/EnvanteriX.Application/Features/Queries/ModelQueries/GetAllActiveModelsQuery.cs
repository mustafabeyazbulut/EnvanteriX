using EnvanteriX.Application.Features.Results.ModelResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.ModelQueries
{
    public class GetAllActiveModelsQuery : IRequest<List<GetAllActiveModelsQueryResult>>
    {
    }
}
