using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyForPets.Domain.SpeciesAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyForPets.Species.Infrastructure.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Species.MAX_NAME_LENGHT);

            builder.HasMany(v => v.Breeds)
                .WithOne()
                .HasForeignKey("species_id")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
