using MediatR;
using EnvanteriX.Application.Features.Results.VendorResults;

namespace EnvanteriX.Application.Features.Queries.VendorQueries
{
    public class GetVendorByIdQuery : IRequest<GetVendorByIdQueryResult>
    {
        public int VendorId { get; set; }

        public GetVendorByIdQuery(int id)
        {
            VendorId = id;
        }
    }
}
