using Entities.Models;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.Configurations;

#nullable disable
public sealed class SpecialistProfileModelConfiguration : IEntityTypeConfiguration<SpecialistProfile>
{
    public void Configure(EntityTypeBuilder<SpecialistProfile> builder)
    {
        builder.ToTable(DbConstants.SpecialistProfileTableName);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);
        builder.Property(x => x.FirstName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Patronymic).HasMaxLength(256);
        builder.Property(x => x.BirthYear).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.CityOfResidence).HasMaxLength(256);
        builder.Property(x => x.Education).HasMaxLength(256);
        builder.Property(x => x.Specialty).HasMaxLength(256);
        builder.Property(x => x.Contact);

        builder.Property(p => p.ProfessionalSkills).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null),
            new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToList())
            );
    }
}
