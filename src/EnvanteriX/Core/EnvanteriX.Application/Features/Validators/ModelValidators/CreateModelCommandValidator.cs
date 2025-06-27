using EnvanteriX.Application.Features.Commands.ModelCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.ModelValidators
{
    
    public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
    {
        public CreateModelCommandValidator()
        {
            RuleFor(x => x.ModelName)
                .NotEmpty().WithMessage("Marka adı boş olamaz.")
                .MinimumLength(3).WithMessage("Marka adı en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Marka adı en fazla 100 karakter olmalıdır.");

            RuleFor(x => x.BrandId)
                .NotEmpty().WithMessage("Marka ID boş olamaz.") // `NotEmpty()` int için çalışmaz, bu yüzden kaldırılmalı
                .GreaterThan(0).WithMessage("Geçerli bir Marka ID giriniz.")
                .WithName("Marka ID");
        }
    }
}
