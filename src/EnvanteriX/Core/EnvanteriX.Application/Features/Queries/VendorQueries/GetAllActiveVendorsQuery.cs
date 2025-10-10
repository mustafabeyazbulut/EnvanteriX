using EnvanteriX.Application.Features.Results.VendorResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.VendorQueries
{
    public class GetAllActiveVendorsQuery :IRequest<List<GetAllActiveVendorsQueryResult>>
    {
    }
}
