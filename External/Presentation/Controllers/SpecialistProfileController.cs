using MediatR;
using AutoMapper;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Core.SpecialistProfiles.Queries.GetSpecialistProfileById;
using Application.Core.SpecialistProfiles.Queries.GetAllSpecialistProfile;
using Application.Core.SpecialistProfiles.Commands.CreateSpecialistProfile;
using Application.Core.SpecialistProfiles.Commands.UpdateSpecialistProfile;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System;

namespace Presentation.Controllers;

[ApiVersion("1.0")]
public sealed class SpecialistProfileController : ApiController
{
    #region ctor
    public ISender Sender { get; init; }
    public IMapper Mapper { get; init; }

    public SpecialistProfileController(ISender sender, IMapper mapper) => (Sender, Mapper) = (sender, mapper);
    #endregion

    #region queries
    [HttpGet("{profileId:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SpecialistProfileFullResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProfile(Guid profileId, CancellationToken token)
    {
        var profile = await Sender.Send(new GetSpecialistProfileByIdQuery(profileId), token);
        return Ok(profile);
    }

    [HttpGet, Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginatedList<SpecialistProfileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllProfiles(CancellationToken token)
    {
        var profiles = await Sender.Send(new GetAllSpecialistProfileQuery(), token);
        return Ok(profiles);
    }
    #endregion

    #region commands
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProfile(CreateSpecialistProfileRequest request, CancellationToken token)
    {
        var command = Mapper.Map<CreateSpecialistProfileCommand>(request);
        var profileId = await Sender.Send(command, token);
        return CreatedAtAction(nameof(GetProfile), new { profileId }, profileId);
    }

    [HttpPatch("{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProfile(Guid profileId, UpdateSpecialistProfileDto dto, CancellationToken token)
    {
        var command = new UpdateSpecialistProfileCommand(profileId, dto);
        _ = await Sender.Send(command, token);
        return NoContent();
    }
    #endregion
}
