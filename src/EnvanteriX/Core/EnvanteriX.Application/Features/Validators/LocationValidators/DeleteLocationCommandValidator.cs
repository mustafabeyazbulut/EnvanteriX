using EnvanteriX.Application.Features.Commands.LocationCommands;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.LocationValidators
{
    public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
    {
        public DeleteLocationCommandValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Lokasyon ID boş olamaz.")
                 .GreaterThan(0).WithMessage("Geçerli bir Lokasyon ID giriniz.")
                 .WithName("Lokasyon ID");
        }
    }
}
