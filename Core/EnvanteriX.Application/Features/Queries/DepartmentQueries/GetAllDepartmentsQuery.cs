using EnvanteriX.Application.Features.Results.DepartmentResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.DepartmentQueries
{
    public class GetAllDepartmentsQuery:IRequest<List<GetAllDepartmentsQueryResult>>
    {
    }
}
