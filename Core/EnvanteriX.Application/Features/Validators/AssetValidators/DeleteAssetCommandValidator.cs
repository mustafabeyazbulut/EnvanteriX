using EnvanteriX.Application.Features.Commands.AssetCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AssetValidators
{
    public class DeleteAssetCommandValidator : AbstractValidator<DeleteAssetCommand>
    {
        public DeleteAssetCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Varlık ID'si boş olamaz.") 
                .GreaterThan(0).WithMessage("Varlık ID'si 0'dan büyük olmalıdır.");
        }
    }
}
