using MediatR;
using EnvanteriX.Application.Features.Results.BrandResults;

namespace EnvanteriX.Application.Features.Queries.BrandQueries
{
    public class GetBrandByIdQuery : IRequest<GetBrandByIdQueryResult>
    {
        public int Id { get; set; }

        public GetBrandByIdQuery(int id)
        {
            Id = id;
        }
    }
}
