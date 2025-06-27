using EnvanteriX.Application.Features.Commands.BrandCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.BrandValidators
{
   public  class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage("Marka adı boş olamaz.")
                .MinimumLength(3).WithMessage("Marka adı en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakter olmalıdır.");
        }
    }
}
