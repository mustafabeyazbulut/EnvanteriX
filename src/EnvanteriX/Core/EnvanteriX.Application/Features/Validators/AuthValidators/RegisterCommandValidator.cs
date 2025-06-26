using EnvanteriX.Application.Features.Commands.AuthCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AuthValidators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("İsim Soyisim");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("E-posta Adresi");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Parola");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(x => x.Password)
                .WithName("Parola Tekrarı");

            RuleFor(x => x.Role)
                 .NotEmpty().WithMessage("Rol boş olamaz.")
                 .MinimumLength(3).WithMessage("Rol en az 3 karakter olmalıdır.")
                 .WithName("Rol");

        }
    }
}
