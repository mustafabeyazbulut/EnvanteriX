using EnvanteriX.Application.Features.Commands.VendorCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.VendorValidators
{
    public class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
    {
        public CreateVendorCommandValidator()
        {
            RuleFor(x => x.VendorName)
                .NotEmpty().WithMessage("Tedarikçi adı boş olamaz.")
                .MaximumLength(100).WithMessage("Tedarikçi adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.ContactPerson)
                .NotEmpty().WithMessage("İletişim kişisi boş olamaz.")
                .MaximumLength(100).WithMessage("İletişim kişisi en fazla 100 karakter olabilir.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?[0-9\s\-()]+$").WithMessage("Geçerli bir telefon numarası giriniz.")
                .MaximumLength(50).WithMessage("Telefon numarası en fazla 50 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta bilgisi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.");
        }
    }
}
