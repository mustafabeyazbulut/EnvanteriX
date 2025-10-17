using EnvanteriX.Application.Features.Results.DepartmentResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.DepartmentQueries
{
    public class GetAllActiveDepartmentsQuery:IRequest<List<GetAllActiveDepartmentsQueryResult>>
    {
    }
}
