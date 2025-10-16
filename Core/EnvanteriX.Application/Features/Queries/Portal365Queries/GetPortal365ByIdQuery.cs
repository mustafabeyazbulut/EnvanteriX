using EnvanteriX.Application.Features.Results.Portal365Results;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.Portal365Queries
{
    public class GetPortal365ByIdQuery:IRequest<GetPortal365ByIdQueryResult>
    {
        public int Id { get; set; }

        public GetPortal365ByIdQuery(int id)
        {
            Id = id;
        }
    }
}
