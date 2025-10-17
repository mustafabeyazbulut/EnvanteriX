using EnvanteriX.Application.Features.Results.DepartmentResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.DepartmentQueries
{
    public class GetDepartmentByIdQuery :IRequest<GetDepartmentByIdQueryResult>
    {
        public int Id { get; set; }

        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
