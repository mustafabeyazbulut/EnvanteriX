using MediatR;
using EnvanteriX.Application.Features.Results.BrandResults;

namespace EnvanteriX.Application.Features.Queries.BrandQueries
{
    public class GetAllBrandsQuery : IRequest<List<GetAllBrandsQueryResult>> { }
}
