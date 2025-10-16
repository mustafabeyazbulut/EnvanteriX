using EnvanteriX.Application.Features.Results.Portal365Results;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.Portal365Queries
{
    public class GetAllPortal365sQuery:IRequest<List<GetAllPortal365sQueryResult>>
    {
    }
}
