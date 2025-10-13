using MediatR;
using EnvanteriX.Application.Features.Results.LocationResults;

namespace EnvanteriX.Application.Features.Queries.LocationQueries
{
    public class GetAllLocationsQuery : IRequest<List<GetAllLocationsQueryResult>> { }
}
