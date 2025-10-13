using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using FluentValidation;


namespace EnvanteriX.Application.Features.Validators.AssetTypeValidators
{
  public  class CreateAssetTypeCommandValidator : AbstractValidator<CreateAssetTypeCommand>
    {
        public CreateAssetTypeCommandValidator()
        {
            RuleFor(x => x.TypeName)
               .NotEmpty().WithMessage("Varlık Tipi adı boş olamaz.")
               .MaximumLength(100).WithMessage("Varlık Tipi en fazla 100 karakter olabilir.");
        }
    }
}
