using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.DepartmentValidators
{
    public class CreateDepartmentCommandValidator:AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
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
