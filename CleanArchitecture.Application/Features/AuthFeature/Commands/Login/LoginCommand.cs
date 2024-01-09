using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeature.Commands.Login;

public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) : IRequest<LoginCommandResponse>;
