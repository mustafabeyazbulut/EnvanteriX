using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.VendorQueries;
using EnvanteriX.Application.Features.Results.VendorResults;
using EnvanteriX.Application.Features.Rules.VendorRules;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class GetVendorByIdQueryHandler : BaseHandler, IRequestHandler<GetVendorByIdQuery, GetVendorByIdQueryResult>
    {
        private readonly VendorRules _vendorRules;
        public GetVendorByIdQueryHandler(Interfaces.AutoMapper.IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, VendorRules vendorRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _vendorRules = vendorRules;
        }
        public async Task<GetVendorByIdQueryResult> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>().GetAsync(x => x.Id == request.Id );
            await _vendorRules.VendorShouldExist(vendor);
            return _mapper.Map<GetVendorByIdQueryResult, Vendor>(vendor);
        }
    }
}
