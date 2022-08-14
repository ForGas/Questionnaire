using Application.Common.Abstractions;
using MediatR;

namespace Application.Core.SpecialistProfiles.Commands.UpdateSpecialistProfile;

public sealed record class UpdateSpecialistProfileCommand(Guid ProfileId, UpdateSpecialistProfileDto dto) : ICommand<Unit>;
