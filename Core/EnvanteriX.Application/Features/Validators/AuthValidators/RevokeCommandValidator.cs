using EnvanteriX.Application.Features.Commands.AuthCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AuthValidators
{
    public class RevokeCommandValidator : AbstractValidator<RevokeCommand>
    {
        public RevokeCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
