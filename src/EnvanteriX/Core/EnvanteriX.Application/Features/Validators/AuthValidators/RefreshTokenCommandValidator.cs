using EnvanteriX.Application.Features.Results.AuthResults;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AuthValidators
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandResult>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();

            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
