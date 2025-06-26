using EnvanteriX.Application.Features.Commands.RoleCommands;
using FluentValidation;


namespace EnvanteriX.Application.Features.Validators.RoleValidators
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleName)
           .NotEmpty().WithMessage("Rol adı boş olamaz.")
           .MinimumLength(3).WithMessage("Rol adı en az 3 karakter olmalıdır.");
        }
    }
}
