using MediatR;
using EnvanteriX.Application.Features.Results.RoleResults;

namespace EnvanteriX.Application.Features.Queries.RoleQueries
{
    public class GetAllRolesQuery : IRequest<List<GetAllRolesQueryResult>>
    {
    }
}
