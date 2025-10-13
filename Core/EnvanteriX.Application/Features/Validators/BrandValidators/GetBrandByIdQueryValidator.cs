using EnvanteriX.Application.Features.Queries.BrandQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.BrandValidators
{
    public class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
    {
        public GetBrandByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Marka ID'si 0'dan büyük olmalıdır.");
        }
    }
}
