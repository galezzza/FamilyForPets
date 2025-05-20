using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyForPets.Domain.Species;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyForPets.Infrastructure.Configurations
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

            builder.OwnsOne(s => s.SpeciesBreeds, sb =>
            {
                sb.ToJson("species_breeds");
                sb.OwnsMany(sbb => sbb.Breeds, bb =>
                {
                    bb.Property(b => b.Name)
                        .IsRequired()
                        .HasColumnName("breed_name")
                        .HasMaxLength(Breed.MAX_NAME_LENGHT);

                    bb.Property(b => b.Id)
                        .HasConversion(
                            id => id.Value,
                            value => BreedId.Create(value));
                });
            });
        }
    }
}
