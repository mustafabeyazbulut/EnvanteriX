using EnvanteriX.Application.Features.Queries.DepartmentQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.DepartmentValidators
{
    public class GetDepartmentByIdQueryValidator :AbstractValidator<GetDepartmentByIdQuery>
    {
        public GetDepartmentByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Departman Id'si 0'dan büyük olmalıdır.");
        }
    }
}
