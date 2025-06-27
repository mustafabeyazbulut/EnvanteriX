using EnvanteriX.Application.Features.Commands.BrandCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.BrandValidators
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Marka ID'si 0'dan büyük olmalıdır.");
        }
    }
}
