﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeature.Commands.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand> 
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.UserNameOrEmail).NotEmpty().WithMessage("Kullanıcı adı yada mail bilgisi boş olamaz.");
            RuleFor(p => p.UserNameOrEmail).NotNull().WithMessage("Kullanıcı adı yada mail bilgisi boş olamaz.");
            RuleFor(p => p.UserNameOrEmail).MinimumLength(3).WithMessage("Kullanıcı adı yada mail en az 3 karakter olmalıdır.");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.Password).NotNull().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir");
            RuleFor(p => p.Password).Matches("[^-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir"); // özel karakter standart yapısı
        }
    }
}
