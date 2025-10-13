using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.MaintenanceRecordValidators
{
  public  class CreateMaintenanceRecordCommandValidator:AbstractValidator<CreateMaintenanceRecordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateMaintenanceRecordCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.AssetId)
                .GreaterThan(0).WithMessage("AssetId geçerli olmalıdır.")
                .MustAsync(async (assetId, cancellationToken) =>
                {
                    var asset = await _unitOfWork.GetReadRepository<Domain.Entities.Asset>().GetAsync(a => a.Id == assetId);
                    return asset != null;
                }).WithMessage("Belirtilen varlık (Asset) bulunamadı.");

            RuleFor(x => x.VendorId)
                .GreaterThan(0).WithMessage("VendorId geçerli olmalıdır.")
                .MustAsync(async (vendorId, cancellationToken) =>
                {
                    var vendor = await _unitOfWork.GetReadRepository<Domain.Entities.Vendor>().GetAsync(v => v.Id == vendorId);
                    return vendor != null;
                }).WithMessage("Belirtilen tedarikçi (Vendor) bulunamadı.");
        }
    }
}
