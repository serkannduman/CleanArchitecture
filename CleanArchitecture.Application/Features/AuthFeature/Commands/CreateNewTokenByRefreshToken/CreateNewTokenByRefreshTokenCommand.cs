using CleanArchitecture.Application.Features.AuthFeature.Commands.Login;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeature.Commands.CreateNewTokenByRefreshToken;

public sealed record CreateNewTokenByRefreshTokenCommand(
    string userId,
    string refreshToken ):IRequest<LoginCommandResponse>;
