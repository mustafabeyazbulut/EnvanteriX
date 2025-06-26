using MediatR;
using EnvanteriX.Application.Features.Results.RoleResults;
using System.Collections.Generic;

namespace EnvanteriX.Application.Features.Queries.RoleQueries
{
    public class GetAllRolesQuery : IRequest<List<GetAllRolesQueryResult>>
    {
    }
}
