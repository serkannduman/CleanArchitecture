using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeature.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string UserName,
    string NameLastName,
    string Password) : IRequest<MessageResponse>
{
}
