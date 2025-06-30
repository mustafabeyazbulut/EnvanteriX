using EnvanteriX.Application.Features.Commands.RoleCommands;
using FluentValidation;


namespace EnvanteriX.Application.Features.Validators.RoleValidators
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Rol ID 1 den küçük olamaz.")
                .GreaterThan(0).WithMessage("Geçerli bir Rol ID giriniz.")
                .WithName("Rol ID");
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Rol adı boş olamaz.")
           .MinimumLength(3).WithMessage("Rol adı en az 3 karakter olmalıdır.");
        }
    }
}
