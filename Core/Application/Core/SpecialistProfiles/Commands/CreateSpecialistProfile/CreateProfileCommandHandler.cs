using AutoMapper;
using Entities.Models;
using Application.Repositories;
using Application.Common.Abstractions;

namespace Application.Core.SpecialistProfiles.Commands.CreateSpecialistProfile;

public sealed class CreateProfileCommandHandler : ICommandHandler<CreateSpecialistProfileCommand, Guid>
{
    public readonly ISpecialistProfileRepository _specialistProfileRepository;
    public readonly IMapper _mapper;

    public CreateProfileCommandHandler(ISpecialistProfileRepository specialistProfileRepository, IMapper mapper)
    {
        _specialistProfileRepository = specialistProfileRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateSpecialistProfileCommand request, CancellationToken cancellationToken)
    {
        var toCreateProfile = _mapper.Map<SpecialistProfile>(request);
        var id = await _specialistProfileRepository.CreateAsync(toCreateProfile);

        return id;
    }
}
