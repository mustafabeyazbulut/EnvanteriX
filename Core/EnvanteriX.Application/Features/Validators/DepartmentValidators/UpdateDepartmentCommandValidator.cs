using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.DepartmentValidators
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Departman Id'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Departman adı boş olamaz.")
                .MaximumLength(255).WithMessage("Departman adı en fazla 255 karakter olabilir.");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Açıklama en fazla 255 karakter olabilir.");
        }
    }
}
