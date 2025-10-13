using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.MaintenanceRecordValidators
{
    class DeleteMaintenanceRecordCommandValidator : AbstractValidator<DeleteMaintenanceRecordCommand>
    {
        public DeleteMaintenanceRecordCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Bakım Kayıt ID'si boş olamaz.")
                .GreaterThan(0).WithMessage("Bakım Kayıt ID'si 0'dan büyük olmalıdır.");
        }
    }
}
