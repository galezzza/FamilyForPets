using System;
using System.Collections.Generic;
using System.Linq;
using FamilyForPets.Species.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeciesEntity = FamilyForPets.Species.Domain.Species;

namespace FamilyForPets.Species.Infrastructure.Configurations
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
                .HasMaxLength(SpeciesEntity.MAX_NAME_LENGHT);
        }
    }

}
