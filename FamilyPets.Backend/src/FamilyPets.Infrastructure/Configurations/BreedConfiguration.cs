using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyForPets.Domain.SpeciesAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyForPets.Infrastructure.Configurations
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breeds");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => BreedId.Create(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Species.MAX_NAME_LENGHT);
        }
    }

}
