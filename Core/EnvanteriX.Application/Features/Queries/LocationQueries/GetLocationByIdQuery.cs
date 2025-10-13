using MediatR;
using EnvanteriX.Application.Features.Results.LocationResults;

namespace EnvanteriX.Application.Features.Queries.LocationQueries
{
    public class GetLocationByIdQuery : IRequest<GetLocationByIdQueryResult>
    {
        public int Id { get; set; }

        public GetLocationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
