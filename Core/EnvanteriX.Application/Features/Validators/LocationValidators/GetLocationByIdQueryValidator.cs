using EnvanteriX.Application.Features.Queries.LocationQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.LocationValidators
{
    public class GetLocationByIdQueryValidator : AbstractValidator<GetLocationByIdQuery>
    {
        public GetLocationByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Lokasyon ID boş olamaz.")
                 .GreaterThan(0).WithMessage("Geçerli bir Lokasyon ID giriniz.")
                 .WithName("Lokasyon ID");
        }
    }
}
