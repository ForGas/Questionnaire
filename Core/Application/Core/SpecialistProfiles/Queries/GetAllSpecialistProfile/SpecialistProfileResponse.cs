using Application.Common.Mappings;
using AutoMapper;
using Entities.Models;

namespace Application.Core.SpecialistProfiles.Queries.GetAllSpecialistProfile;

public sealed record class SpecialistProfileResponse : IMapFrom<SpecialistProfile>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthYear { get; set; }
    public string? Specialty { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SpecialistProfile, SpecialistProfileResponse>();
    }
}
