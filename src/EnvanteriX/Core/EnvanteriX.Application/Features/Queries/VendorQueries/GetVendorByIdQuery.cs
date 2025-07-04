using MediatR;
using EnvanteriX.Application.Features.Results.VendorResults;

namespace EnvanteriX.Application.Features.Queries.VendorQueries
{
    public class GetVendorByIdQuery : IRequest<GetVendorByIdQueryResult>
    {
        public int Id { get; set; }

        public GetVendorByIdQuery(int id)
        {
           this.Id = id;
        }
    }
}
