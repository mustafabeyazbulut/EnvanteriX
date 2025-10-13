using EnvanteriX.Application.Features.Commands.UserCommands;
using FluentValidation;


namespace EnvanteriX.Application.Features.Validators.UserValidators
{
    public class RemoveUserRoleCommandValidator : AbstractValidator<RemoveUserRoleCommand>
    {
        public RemoveUserRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.") // `NotEmpty()` int için çalışmaz, bu yüzden kaldırılmalı
                 .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID giriniz.")
                 .WithName("Kullanıcı ID");

            RuleFor(x => x.RoleName)
         .NotEmpty().WithMessage("Rol adı boş olamaz.")
         .MinimumLength(3).WithMessage("Rol adı en az 3 karakter olmalıdır.");
        }
    }
}
