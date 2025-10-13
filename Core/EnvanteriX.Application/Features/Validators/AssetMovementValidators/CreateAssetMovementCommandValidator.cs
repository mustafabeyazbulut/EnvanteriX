using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace EnvanteriX.Application.Features.Validators.AssetMovementValidators
{
    public class CreateAssetMovementCommandValidator : AbstractValidator<CreateAssetMovementCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public CreateAssetMovementCommandValidator(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

            RuleFor(x => x.AssetId)
                .GreaterThan(0).WithMessage("AssetId geçerli olmalıdır.")
                .MustAsync(async (assetId, cancellationToken) =>
                {
                    var asset = await _unitOfWork.GetReadRepository<Asset>().GetAsync(a => a.Id == assetId);
                    return asset != null;
                }).WithMessage("Belirtilen varlık (Asset) bulunamadı.");

            RuleFor(x => x.FromUserId)
                .MustAsync(async (userId, cancellationToken) =>
                {
                    if (!userId.HasValue) return true;
                    var user = await _userManager.FindByIdAsync(userId.Value.ToString());
                    return user != null;
                }).WithMessage("Gönderen kullanıcı bulunamadı.");

            RuleFor(x => x.ToUserId)
                .MustAsync(async (userId, cancellationToken) =>
                {
                    if (!userId.HasValue) return true;
                    var user = await _userManager.FindByIdAsync(userId.Value.ToString());
                    return user != null;
                }).WithMessage("Alan kullanıcı bulunamadı.");

            RuleFor(x => x.FromLocationId)
                .MustAsync(async (locationId, cancellationToken) =>
                {
                    if (!locationId.HasValue) return true;
                    var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(l => l.Id == locationId.Value);
                    return location != null;
                }).WithMessage("Gönderen konum bulunamadı.");

            RuleFor(x => x.ToLocationId)
                .MustAsync(async (locationId, cancellationToken) =>
                {
                    if (!locationId.HasValue) return true;
                    var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(l => l.Id == locationId.Value);
                    return location != null;
                }).WithMessage("Alan konum bulunamadı.");

            RuleFor(x => x.Note)
                .MaximumLength(255).WithMessage("Not alanı en fazla 255 karakter olabilir.");
        }
    }
}
