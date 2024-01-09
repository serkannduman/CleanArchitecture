using CleanArchitecture.Application.Features.AuthFeature.Commands.Login;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Abstractions;

public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateTokenAsync(User user);
}
