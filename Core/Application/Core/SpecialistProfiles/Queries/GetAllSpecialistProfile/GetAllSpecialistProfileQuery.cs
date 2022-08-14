using Application.Common.Abstractions;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Core.SpecialistProfiles.Queries.GetAllSpecialistProfile;

public sealed class GetAllSpecialistProfileQuery : PaginatedQuery, IQuery<PaginatedList<SpecialistProfileResponse>> { }

public record class GetAllSpecialistProfileQueryHandler
    : IQueryHandler<GetAllSpecialistProfileQuery, PaginatedList<SpecialistProfileResponse>>
{
    private readonly ISpecialistProfileRepository _specialistProfileRepository;
    private readonly IMapper _mapper;

    public GetAllSpecialistProfileQueryHandler(
        ISpecialistProfileRepository specialistProfileRepository,
        IMapper mapper)
    {
        _specialistProfileRepository = specialistProfileRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SpecialistProfileResponse>> Handle(GetAllSpecialistProfileQuery request, CancellationToken cancellationToken)
    {
        return await _specialistProfileRepository.GetAllQueryable()
                       .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                       .ProjectTo<SpecialistProfileResponse>(_mapper.ConfigurationProvider)
                       .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
