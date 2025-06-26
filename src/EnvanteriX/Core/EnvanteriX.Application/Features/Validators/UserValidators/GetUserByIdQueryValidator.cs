using EnvanteriX.Application.Features.Queries.UserQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.UserValidators
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.") // `NotEmpty()` int için çalışmaz, bu yüzden kaldırılmalı
                 .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID giriniz.")
                 .WithName("Kullanıcı ID");
        }
    }
}
