using Application.Core.Authentification.Commands;
using Application.Core.Authentification.Commands.Login;
using Application.Core.Authentification.Commands.Register;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Presentation.Controllers;

[ApiVersion("1.0")]
[Route("auth")]
public class AuthentificationController : ApiController
{
    #region ctor
    public ISender Sender { get; init; }
    public IMapper Mapper { get; init; }

    public AuthentificationController(ISender sender, IMapper mapper) => (Sender, Mapper) = (sender, mapper);
    #endregion

    #region command
    [HttpPost, AllowAnonymous]
    [Route("register")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(AuthentificationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return Created("", await Sender.Send(new RegisterCommand(
            FirstName: request.FirstName,
            LastName: request.LastName,
            Email: request.Email,
            Password: request.Password)
            ));
    }

    [HttpPost, AllowAnonymous]
    [Route("login")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(AuthentificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await Sender.Send(new LoginCommand(
            Email: request.Email,
            Password: request.Password)
        ));
    }
    #endregion
}