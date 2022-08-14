using Entities.Primitives;

namespace Entities.Models;

public class SpecialistProfile : Entity
{
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
}