using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.MaintenanceRecordValidators
{
    public class UpdateMaintenanceRecordCommandValidator : AbstractValidator<UpdateMaintenanceRecordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateMaintenanceRecordCommandValidator(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Geçersiz kayıt ID.");


            RuleFor(x => x.PerformedBy)
                .MaximumLength(100).WithMessage("Bakımı yapan alanı en fazla 100 karakter olabilir.");

            RuleFor(x => x.PreServiceDescription)
                .MaximumLength(255).WithMessage("Servis öncesi açıklama en fazla 255 karakter olabilir.");

            RuleFor(x => x.PostServiceDescription)
                .MaximumLength(255).WithMessage("Servis sonrası açıklama en fazla 255 karakter olabilir.");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).When(x => x.Cost != default)
                .WithMessage("Bakım maliyeti negatif olamaz.");

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
