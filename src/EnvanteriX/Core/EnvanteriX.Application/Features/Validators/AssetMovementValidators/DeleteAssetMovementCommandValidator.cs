using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AssetMovementValidators
{
    class DeleteAssetMovementCommandValidator: AbstractValidator<DeleteAssetMovementCommand>
    {
        public DeleteAssetMovementCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Haraket ID'si boş olamaz.")
                .GreaterThan(0).WithMessage("Haraket ID'si 0'dan büyük olmalıdır.");
        }
    }
}
