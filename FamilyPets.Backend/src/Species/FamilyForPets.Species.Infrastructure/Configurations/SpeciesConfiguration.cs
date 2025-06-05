using FamilyForPets.Species.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeciesEntity = FamilyForPets.Species.Domain.Species;

namespace FamilyForPets.Species.Infrastructure.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<SpeciesEntity>
    {
        public void Configure(EntityTypeBuilder<SpeciesEntity> builder)
        {
            builder.ToTable("species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(SpeciesEntity.MAX_NAME_LENGHT);

            builder.HasMany(v => v.Breeds)
                .WithOne()
                .HasForeignKey("species_id")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
