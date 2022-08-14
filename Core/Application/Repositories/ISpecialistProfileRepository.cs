using Entities.Models;
using Application.Core.SpecialistProfiles.Commands.UpdateSpecialistProfile;

namespace Application.Repositories;

public interface ISpecialistProfileRepository
{
    Task<Guid> CreateAsync(SpecialistProfile newEntity);
    Task UpdateAsync(Guid profileId, UpdateSpecialistProfileDto profile);

    Task<SpecialistProfile> GetByIdAsync(Guid profileId);
    IQueryable<SpecialistProfile> GetAllQueryable();
}
