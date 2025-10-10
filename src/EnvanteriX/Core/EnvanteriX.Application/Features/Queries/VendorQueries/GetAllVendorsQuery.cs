using MediatR;
using EnvanteriX.Application.Features.Results.VendorResults;

namespace EnvanteriX.Application.Features.Queries.VendorQueries
{
    public class GetAllVendorsQuery : IRequest<List<GetAllVendorsQueryResult>>
    {
    }
}
