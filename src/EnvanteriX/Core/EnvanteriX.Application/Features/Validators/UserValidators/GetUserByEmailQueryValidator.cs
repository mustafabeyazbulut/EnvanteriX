using EnvanteriX.Application.Features.Queries.UserQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.UserValidators
{
    public class GetUserByEmailQueryValidator: AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("E-posta Adresi");
        }
    }
}
