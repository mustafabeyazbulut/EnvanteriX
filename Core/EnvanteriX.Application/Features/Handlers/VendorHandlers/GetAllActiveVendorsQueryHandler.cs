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
    public class GetAllActiveVendorsQueryHandler : BaseHandler, IRequestHandler<GetAllActiveVendorsQuery, List<GetAllActiveVendorsQueryResult>>
    {
        public GetAllActiveVendorsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveVendorsQueryResult>> Handle(GetAllActiveVendorsQuery request, CancellationToken cancellationToken)
        {
            var vendors = await _unitOfWork.GetReadRepository<Vendor>().GetAllAsync(x=>x.IsDeleted==false);
            return _mapper.Map<GetAllActiveVendorsQueryResult, Vendor>(vendors).ToList();
        }
    }
}
