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
           .NotEmpty().WithMessage("Yeni parola boş olamaz.")
           .MinimumLength(8).WithMessage("Parola en az 8 karakter olmalıdır.")
           .Matches("[A-Z]").WithMessage("Parola en az bir büyük harf içermelidir.")
           .Matches("[a-z]").WithMessage("Parola en az bir küçük harf içermelidir.")
           .Matches("[0-9]").WithMessage("Parola en az bir rakam içermelidir.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Parola en az bir özel karakter içermelidir.")
           .WithName("Yeni Parola");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Parola tekrarı boş olamaz.")
                .Equal(x => x.Password).WithMessage("Parolalar uyuşmuyor.")
                .WithName("Parola Tekrarı");

            RuleFor(x => x.Role)
                 .NotEmpty().WithMessage("Rol boş olamaz.")
                 .MinimumLength(3).WithMessage("Rol en az 3 karakter olmalıdır.")
                 .WithName("Rol");

        }
    }
}
