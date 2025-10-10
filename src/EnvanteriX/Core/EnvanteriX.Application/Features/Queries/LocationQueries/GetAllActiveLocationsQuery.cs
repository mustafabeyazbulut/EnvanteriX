using EnvanteriX.Application.Features.Results.LocationResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.LocationQueries
{
    public class GetAllActiveLocationsQuery:IRequest<List<GetAllActiveLocationsQueryResult>>
    {
    }
}
