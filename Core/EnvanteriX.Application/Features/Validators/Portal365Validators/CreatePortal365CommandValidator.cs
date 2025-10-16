using EnvanteriX.Application.Features.Commands.Portal365Commands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.Portal365Validators
{
    public class CreatePortal365CommandValidator : AbstractValidator<CreatePortal365Command>
    {
        public CreatePortal365CommandValidator()
        {
            RuleFor(x => x.TenantId)
             .NotEmpty().WithMessage("TenantId alanı boş olamaz.")
             .MaximumLength(100).WithMessage("TenantId 100 karakterden uzun olamaz.");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId alanı boş olamaz.")
                .MaximumLength(100).WithMessage("ClientId 100 karakterden uzun olamaz.");

            RuleFor(x => x.ClientSecret)
                .NotEmpty().WithMessage("ClientSecret alanı boş olamaz.")
                .MinimumLength(10).WithMessage("ClientSecret en az 10 karakter olmalıdır.");

            RuleFor(x => x.SenderEmail)
                .NotEmpty().WithMessage("Gönderici e-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
