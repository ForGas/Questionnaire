using MediatR;
using Application.Repositories;
using Application.Common.Abstractions;
using Application.Common.Exceptions;

namespace Application.Core.SpecialistProfiles.Commands.UpdateSpecialistProfile;

public sealed class UpdateSpecialistProfileCommandHandler : ICommandHandler<UpdateSpecialistProfileCommand, Unit>
{
    public readonly ISpecialistProfileRepository _specialistProfileRepository;

    public UpdateSpecialistProfileCommandHandler(ISpecialistProfileRepository specialistProfileRepository)
        => _specialistProfileRepository = specialistProfileRepository;

    public async Task<Unit> Handle(UpdateSpecialistProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.ProfileId != request.dto.Id)
        {
            throw new BadRequestException(nameof(request), request.ProfileId);
        }

        await _specialistProfileRepository.UpdateAsync(request.ProfileId, request.dto);
        return Unit.Value;
    }
}
