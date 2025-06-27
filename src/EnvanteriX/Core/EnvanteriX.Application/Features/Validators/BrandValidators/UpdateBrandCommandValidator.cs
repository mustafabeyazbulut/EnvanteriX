using EnvanteriX.Application.Features.Commands.BrandCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.BrandValidators
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage("Marka ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("Marka adı boş olamaz.")
                .MinimumLength(3).WithMessage("Marka adı en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakter olmalıdır.");
        }
    }
}
