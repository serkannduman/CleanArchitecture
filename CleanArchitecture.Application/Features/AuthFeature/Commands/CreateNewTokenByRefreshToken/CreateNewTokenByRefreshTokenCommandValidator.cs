using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeature.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenByRefreshTokenCommandValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
{
    public CreateNewTokenByRefreshTokenCommandValidator()
    {
        RuleFor(p => p.userId).NotEmpty().WithMessage("User bilgisi boş olamaz");
        RuleFor(p => p.userId).NotNull().WithMessage("User bilgisi boş olamaz");
        RuleFor(p => p.refreshToken).NotNull().WithMessage("Refresh Token bilgisi boş olamaz");
        RuleFor(p => p.refreshToken).NotEmpty().WithMessage("Refresh Token bilgisi boş olamaz");
    }
}
