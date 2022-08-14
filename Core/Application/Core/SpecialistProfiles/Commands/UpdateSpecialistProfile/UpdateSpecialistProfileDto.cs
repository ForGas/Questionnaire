using AutoMapper;
using Entities.Models;
using Application.Common.Mappings;

namespace Application.Core.SpecialistProfiles.Commands.UpdateSpecialistProfile;

public sealed record class UpdateSpecialistProfileDto : IMapFrom<SpecialistProfile>
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
        profile.CreateMap<UpdateSpecialistProfileDto, SpecialistProfile>();
    }
}
