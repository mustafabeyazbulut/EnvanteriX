using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AssetTypeValidators
{
    public class UpdateAssetTypeCommandValidator : AbstractValidator<UpdateAssetTypeCommand>
    {
        public UpdateAssetTypeCommandValidator()
        {

            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Varlık Tipi ID boş olamaz.")
                 .GreaterThan(0).WithMessage("Geçerli bir Varlık Tipi ID giriniz.")
                 .WithName("Varlık Tipi ID");

            RuleFor(x => x.TypeName)
                .NotEmpty().WithMessage("Varlık Tipi adı boş olamaz.")
                .MaximumLength(100).WithMessage("Varlık Tipi en fazla 100 karakter olabilir.");
        }
    }
}
