using MediatR;
using EnvanteriX.Application.Features.Results.RoleResults;

namespace EnvanteriX.Application.Features.Queries.RoleQueries
{
    public class GetRoleByIdQuery : IRequest<GetRoleByIdQueryResult>
    {
        public int RoleId { get; set; }
        public GetRoleByIdQuery(int id) => RoleId = id;
    }
}
