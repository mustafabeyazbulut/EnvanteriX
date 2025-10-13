using EnvanteriX.Application.Features.Commands.VendorCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.VendorValidators
{
    public class DeleteVendorCommandValidator : AbstractValidator<DeleteVendorCommand>
    {
        public DeleteVendorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Tedarikçi ID'si 0'dan büyük olmalıdır.")
                .WithMessage("Tedarikçi ID'si boş olamaz.");
        }
    }
}
