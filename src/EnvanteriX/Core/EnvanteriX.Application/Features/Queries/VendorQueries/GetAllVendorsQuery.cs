using MediatR;
using EnvanteriX.Application.Features.Results.VendorResults;
using System.Collections.Generic;

namespace EnvanteriX.Application.Features.Queries.VendorQueries
{
    public class GetAllVendorsQuery : IRequest<List<GetAllVendorsQueryResult>>
    {
    }
}
