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
    public class DeleteVendorCommandHandler : BaseHandler, IRequestHandler<DeleteVendorCommand, Unit>
    {
        private readonly VendorRules _vendorRules;
        public DeleteVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, VendorRules vendorRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _vendorRules = vendorRules;
        }

        public async Task<Unit> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>().GetAsync(x => x.Id == request.Id);
            await _vendorRules.VendorShouldExist(vendor);
            var hasAnyAsset = await _unitOfWork.GetReadRepository<Asset>().AnyAsync(x => x.VendorId == request.Id );
            await _vendorRules.VendorShouldNotHaveAnyAsset(hasAnyAsset, vendor.VendorName);

            var hasAnySoftwareLicense = await _unitOfWork.GetReadRepository<SoftwareLicense>().AnyAsync(x => x.VendorId == request.Id );
            await _vendorRules.VendorShouldNotHaveAnySoftwareLicense(hasAnySoftwareLicense, vendor.VendorName);

            var hasAnyMaintenanceRecord = await _unitOfWork.GetReadRepository<MaintenanceRecord>().AnyAsync(x => x.VendorId == request.Id );
            await _vendorRules.VendorShouldNotHaveAnyMaintenanceRecord(hasAnyMaintenanceRecord, vendor.VendorName);

            await _unitOfWork.GetWriteRepository<Vendor>().HardDeleteAsync(vendor);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
