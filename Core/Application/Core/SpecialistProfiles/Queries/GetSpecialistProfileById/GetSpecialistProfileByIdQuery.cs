using Application.Repositories;
using AutoMapper;
using Application.Common.Abstractions;

namespace Application.Core.SpecialistProfiles.Queries.GetSpecialistProfileById;

public sealed record class GetSpecialistProfileByIdQuery(Guid profileId) : IQuery<SpecialistProfileFullResponse>;

public sealed class GetProfileByIdQueryHandler : IQueryHandler<GetSpecialistProfileByIdQuery, SpecialistProfileFullResponse>
{
    private readonly ISpecialistProfileRepository _specialistProfileRepository;
    private readonly IMapper _mapper;

    public GetProfileByIdQueryHandler(
        ISpecialistProfileRepository specialistProfileRepository,
        IMapper mapper)
    {
        _specialistProfileRepository = specialistProfileRepository;
        _mapper = mapper;
    }

    public async Task<SpecialistProfileFullResponse> Handle(GetSpecialistProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var profile = await _specialistProfileRepository.GetByIdAsync(request.profileId);
        return _mapper.Map<SpecialistProfileFullResponse>(profile);
    }
}
