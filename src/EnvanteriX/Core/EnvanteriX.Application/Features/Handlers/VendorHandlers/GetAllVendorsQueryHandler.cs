using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.VendorQueries;
using EnvanteriX.Application.Features.Results.VendorResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class GetAllVendorsQueryHandler : BaseHandler, IRequestHandler<GetAllVendorsQuery, List<GetAllVendorsQueryResult>>
    {
        public GetAllVendorsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllVendorsQueryResult>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            var vendors = await _unitOfWork.GetReadRepository<Vendor>().GetAllAsync();
            return _mapper.Map<GetAllVendorsQueryResult, Vendor>(vendors).ToList();
        }
    }
}
