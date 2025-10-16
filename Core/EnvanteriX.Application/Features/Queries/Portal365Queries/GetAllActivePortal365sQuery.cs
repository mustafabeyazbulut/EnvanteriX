using EnvanteriX.Application.Features.Results.Portal365Results;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.Portal365Queries
{
    public class GetAllActivePortal365sQuery:IRequest<List<GetAllActivePortal365sQueryResult>>
    {
    }
}
