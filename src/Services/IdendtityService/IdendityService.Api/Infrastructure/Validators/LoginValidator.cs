﻿using FluentValidation;
using IdentityService.Api.Core.Domain;

namespace IdentityService.Api.Infrastructure.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz!")
                .MinimumLength(3).WithMessage("Kullanıcı Adı En Az 3 Karakter İçermelidir.");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter içermelidir.")
                .Must(password => password.Any(char.IsUpper)).WithMessage("Parolada en az bir büyük harf olmalıdır.")
                .Must(password => password.Any(char.IsDigit)).WithMessage("Parolada en az bir rakam olmalıdır.");
        }
    }
}
