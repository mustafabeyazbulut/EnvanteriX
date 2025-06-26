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
    public class GetVendorByIdQueryHandler : BaseHandler, IRequestHandler<GetVendorByIdQuery, GetVendorByIdQueryResult>
    {
        public GetVendorByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetVendorByIdQueryResult> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>()
                .GetAsync(x => x.Id == request.VendorId && !x.IsDeleted);

            if (vendor == null) 
                throw new KeyNotFoundException($"Vendor with ID {request.VendorId} not found.");

            var result = _mapper.Map<GetVendorByIdQueryResult>(vendor);
            return result;
        }
    }
}
