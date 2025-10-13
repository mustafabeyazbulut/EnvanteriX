using EnvanteriX.Application.Features.Results.RoleResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.RoleQueries
{
    public class GetAllActiveRolesQuery : IRequest<List<GetAllActiveRolesQueryResult>>
    {
    }
}
