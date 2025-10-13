using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AssetTypeValidators
{
    public class DeleteAssetTypeCommandValidator : AbstractValidator<DeleteAssetTypeCommand>
    {
        public DeleteAssetTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Varlık Tipi ID boş olamaz.")
                 .GreaterThan(0).WithMessage("Geçerli bir Varlık Tipi ID giriniz.")
                 .WithName("Varlık Tipi ID");
        }
    }
}
