using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Validators.AssetValidators
{
    public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public CreateAssetCommandValidator(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

            RuleFor(x => x.AssetTag)
                .NotEmpty().WithMessage("Asset Tag boş olamaz.")
                .MaximumLength(50).WithMessage("Asset Tag en fazla 50 karakter olabilir.");

            RuleFor(x => x.SerialNumber)
                .MaximumLength(100).WithMessage("Seri numarası en fazla 100 karakter olabilir.");

            RuleFor(x => x.AssetTypeId)
                .GreaterThan(0).WithMessage("Asset Type seçilmelidir.")
                .MustAsync(AssetTypeExists).WithMessage("Geçersiz AssetTypeId.");

            RuleFor(x => x.ModelId)
                .GreaterThan(0).WithMessage("Model seçilmelidir.")
                .MustAsync(ModelExists).WithMessage("Geçersiz ModelId.");

            RuleFor(x => x.VendorId)
                .GreaterThan(0).WithMessage("Vendor seçilmelidir.")
                .MustAsync(VendorExists).WithMessage("Geçersiz VendorId.");

            RuleFor(x => x.LocationId)
                .GreaterThan(0).WithMessage("Lokasyon seçilmelidir.")
                .MustAsync(LocationExists).WithMessage("Geçersiz LocationId.");

            RuleFor(x => x.AssignedUserId)
                .MustAsync(AssignedUserExistsOrNull).WithMessage("Geçersiz AssignedUserId.");

            RuleFor(x => x.PurchaseDate)
                .NotEmpty().WithMessage("Satın alma tarihi boş olamaz.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Satın alma tarihi bugünden büyük olamaz.");

            RuleFor(x => x.WarrantyEndDate)
                .NotEmpty().WithMessage("Garanti bitiş tarihi boş olamaz.")
                .GreaterThanOrEqualTo(x => x.PurchaseDate).WithMessage("Garanti bitiş tarihi satın alma tarihinden küçük olamaz.");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Maliyet negatif olamaz.");

            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("Açıklama en fazla 255 karakter olabilir.");

        }

        private async Task<bool> AssetTypeExists(int assetTypeId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetReadRepository<AssetType>().AnyAsync(at => at.Id == assetTypeId);
        }

        private async Task<bool> ModelExists(int modelId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetReadRepository<Model>().AnyAsync(m => m.Id == modelId);
        }

        private async Task<bool> VendorExists(int vendorId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetReadRepository<Vendor>().AnyAsync(v => v.Id == vendorId);
        }

        private async Task<bool> LocationExists(int locationId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetReadRepository<Location>().AnyAsync(l => l.Id == locationId);
        }

        private async Task<bool> AssignedUserExistsOrNull(int? assignedUserId, CancellationToken cancellationToken)
        {
            if (!assignedUserId.HasValue)
                return true;

            var user = await _userManager.FindByIdAsync(assignedUserId.Value.ToString());
            return user != null;
        }
    }

}
