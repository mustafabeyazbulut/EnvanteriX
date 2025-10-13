using EnvanteriX.Application.Features.Commands.RoleCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.RoleValidators
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Rol ID boş olamaz.") 
                 .GreaterThan(0).WithMessage("Geçerli bir Rol ID giriniz.")
                 .WithName("Rol ID");
        }
    }
}
