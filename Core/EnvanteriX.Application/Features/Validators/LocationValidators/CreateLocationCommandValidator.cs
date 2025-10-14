using EnvanteriX.Application.Features.Commands.LocationCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.LocationValidators
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Building)
                 .NotEmpty().WithMessage("Bina adı boş olamaz.")
                 .MaximumLength(100).WithMessage("Bina adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Açıklama en fazla 255 karakter olabilir.");

        }
    }
}
