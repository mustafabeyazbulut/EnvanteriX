using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Features.Rules.VendorRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class ActiveVendorCommandHandler : BaseHandler, IRequestHandler<ActiveVendorCommand, Unit>
    {
        private readonly VendorRules _vendorRules;
        public ActiveVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, VendorRules vendorRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _vendorRules = vendorRules;
        }

        public async Task<Unit> Handle(ActiveVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>().GetAsync(x => x.Id == request.Id);
            await _vendorRules.VendorShouldExist(vendor);

            vendor.IsDeleted = false;
            await _unitOfWork.GetWriteRepository<Vendor>().UpdateAsync(vendor);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
