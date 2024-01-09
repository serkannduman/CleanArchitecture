using CleanArchitecture.Application.Features.AuthFeature.Commands.CreateNewTokenByRefreshToken;
using CleanArchitecture.Application.Features.AuthFeature.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeature.Commands.Register;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register (RegisterCommand request, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(request,cancellationToken);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginCommandResponse response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            LoginCommandResponse response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
