using EnvanteriX.Application.Features.Results.ModelResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.ModelQueries
{
    public class GetAllActiveModelByBrandIdQuery: IRequest<List<GetAllActiveModelByBrandIdQueryResult>>
    {
        public int BrandId { get; set; }
        public GetAllActiveModelByBrandIdQuery(int id)
        {
            BrandId = id;
        }
    }
}
