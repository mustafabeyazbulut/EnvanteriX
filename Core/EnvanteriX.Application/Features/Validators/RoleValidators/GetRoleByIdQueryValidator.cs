using EnvanteriX.Application.Features.Queries.RoleQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.RoleValidators
{
    public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidator()
        {
            RuleFor(x => x.RoleId)
                 .NotEmpty().WithMessage("Rol ID boş olamaz.")
                 .GreaterThan(0).WithMessage("Geçerli bir Rol ID giriniz.")
                 .WithName("Rol ID");
        }
    }
}
