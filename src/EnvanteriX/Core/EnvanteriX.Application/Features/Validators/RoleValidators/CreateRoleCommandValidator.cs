using EnvanteriX.Application.Features.Commands.RoleCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.RoleValidators
{
   public  class CreateRoleCommandValidator :AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Rol adı boş olamaz.")
           .MinimumLength(3).WithMessage("Rol adı en az 3 karakter olmalıdır.");
        }
    }
}
