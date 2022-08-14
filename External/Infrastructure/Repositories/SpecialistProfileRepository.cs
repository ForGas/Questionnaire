using AutoMapper;
using Entities.Models;
using Infrastructure.Data;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Application.Core.SpecialistProfiles.Commands.UpdateSpecialistProfile;

namespace Infrastructure.Repositories;

public sealed class SpecialistProfileRepository : ISpecialistProfileRepository
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SpecialistProfileRepository(
        IApplicationDbContext context,
        IMapper mapper
        )
        => (_context, _mapper) = (context, mapper);

    public async Task<Guid> CreateAsync(SpecialistProfile newEntity)
    {
        await _context.SpecialistProfiles.AddAsync(newEntity);
        await _context.SaveChangesAsync();

        return newEntity.Id;
    }

    public async Task<SpecialistProfile> GetByIdAsync(Guid profileId)
    {
        var profile = await _context.SpecialistProfiles.SingleOrDefaultAsync(x => x.Id == profileId);
        return profile ?? throw new EntityNotFoundException(nameof(profile), profileId);
    }

    public async Task UpdateAsync(Guid profileId, UpdateSpecialistProfileDto profile)
    {
        var toUpdateProfile = await _context.SpecialistProfiles.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == profileId);

        if (toUpdateProfile != null)
        {
            toUpdateProfile = _mapper.Map<SpecialistProfile>(profile);
            _context.SpecialistProfiles.Update(toUpdateProfile);
            await _context.SaveChangesAsync();
            return;
        }

        throw new EntityNotFoundException(nameof(profile), profileId);
    }

    public IQueryable<SpecialistProfile> GetAllQueryable()
    {
        return _context.SpecialistProfiles.AsQueryable();
    }
}
