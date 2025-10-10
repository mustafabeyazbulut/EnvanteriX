using EnvanteriX.Application.Features.Commands.UserCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvanteriX.Application.Features.Validators.UserValidators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.") // `NotEmpty()` int için çalışmaz, bu yüzden kaldırılmalı
                 .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID giriniz.")
                 .WithName("Kullanıcı ID");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Yeni parola boş olamaz.")
            .MinimumLength(8).WithMessage("Parola en az 8 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("Parola en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Parola en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Parola en az bir rakam içermelidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Parola en az bir özel karakter içermelidir.")
            .WithName("Yeni Parola");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Parola tekrarı boş olamaz.")
                .Equal(x => x.Password).WithMessage("Parolalar uyuşmuyor.")
                .WithName("Parola Tekrarı");

        }
    }
}
