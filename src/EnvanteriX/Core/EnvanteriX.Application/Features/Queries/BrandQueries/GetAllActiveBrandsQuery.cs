using EnvanteriX.Application.Features.Results.BrandResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.BrandQueries
{
    public class GetAllActiveBrandsQuery : IRequest<List<GetAllActiveBrandsQueryResult>> { }
}
