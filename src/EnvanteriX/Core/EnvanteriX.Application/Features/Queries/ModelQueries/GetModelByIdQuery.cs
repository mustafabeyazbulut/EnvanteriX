using MediatR;
using EnvanteriX.Application.Features.Results.ModelResults;

namespace EnvanteriX.Application.Features.Queries.ModelQueries
{
    public class GetModelByIdQuery : IRequest<GetModelByIdQueryResult>
    {
        public int Id { get; set; }
        public GetModelByIdQuery(int id)
        {
            Id = id;
        }
    }
}
