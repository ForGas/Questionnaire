using Application.Common.Mappings;
using AutoMapper;
using Entities.Models;

namespace Application.Core.SpecialistProfiles.Queries.GetSpecialistProfileById;

public sealed record class SpecialistProfileFullResponse : IMapFrom<SpecialistProfile>
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public string? Contact { get; set; }
    public DateTime BirthYear { get; set; }
    public string? Education { get; set; }
    public string? Specialty { get; set; }
    public string? CityOfResidence { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; }
    public List<string>? ProfessionalSkills { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SpecialistProfile, SpecialistProfileFullResponse>();
    }
}
