using System.Text.Json;
using FamilyForPets.Core.Configurations.Converters;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Contracts.DTOs;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.PetValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyForPets.Volunteers.Infrastructure.Configurations.Read
{
    public class PetDTOConfiguration : IEntityTypeConfiguration<PetDTO>
    {
        public void Configure(EntityTypeBuilder<PetDTO> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id);

            builder.Property(p => p.VolunteerId)
                .HasColumnName("volunteer_id");

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(PetNickname.MAX_NAME_LENGHT);

            builder.Property(p => p.Description)
                .HasColumnName("pet_description")
                .IsRequired(false)
                .HasMaxLength(PetDescription.MAX_DESCRIPTION_LENGHT);

            builder.Property(p => p.PrimaryColor)
                .HasColumnName("primary_color")
                .IsRequired()
                .HasConversion<ColorToInt32Converter>();

            builder.Property(p => p.SecondaryColor)
                .HasColumnName("secondary_color")
                .IsRequired(false)
                .HasConversion<ColorToInt32Converter>();

            builder.Property(p => p.TertiaryColor)
                .HasColumnName("tertiary_color")
                .IsRequired(false)
                .HasConversion<ColorToInt32Converter>();

            builder.Property(p => p.DateOfBirth)
                .HasColumnName("date_of_birth")
                .IsRequired(false)
                .HasConversion(new DateTimeToBinaryConverter());

            builder.Property(p => p.BreedId)
                    .HasColumnName("breed_id")
                    .IsRequired();

            builder.Property(p => p.SpeciesId)
                    .HasColumnName("species_id")
                    .IsRequired();

            builder.Property(p => p.PetHealthDescription)
                .HasColumnName("pet_health_description")
                .IsRequired(false)
                .HasMaxLength(PetHealthDescription.MAX_DESCRIPTION_LENGHT);

            builder.Property(p => p.Country)
                .HasColumnName("country")
                .IsRequired(false)
                .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);

            builder.Property(p => p.City)
                .HasColumnName("city")
                .IsRequired(false)
                .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);

            builder.Property(p => p.Street)
                .HasColumnName("street")
                .IsRequired(false)
                .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);

            builder.Property(p => p.HouseNumber)
                .HasColumnName("house_number")
                .IsRequired(false)
                .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);

            builder.Property(p => p.Weight)
                .HasColumnName("weight");

            builder.Property(p => p.MassType)
                .HasColumnName("mass_type_enum")
                .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);

            // Weight can be null, so its properties Value and MassType set as not required
            // but if Weight is not null, then MassType are required.
            // So there is a check constraint
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Weight_BothOrNone",
                    "(weight IS NULL AND mass_type_enum IS NULL) OR (weight IS NOT NULL AND mass_type_enum IS NOT NULL)");
            });

            builder.Property(p => p.Height)
                .HasColumnName("height");

            builder.Property(p => p.LengthType)
                .HasColumnName("length_type_enum")
                .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);

            // Height can be null, so its properties Value and LengthType set as not required
            // but if Height is not null, then Value and LengthType are required.
            // So there is a check constraint
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Height_BothOrNone",
                    "(height IS NULL AND length_type_enum IS NULL) OR (height IS NOT NULL AND length_type_enum IS NOT NULL)");
            });

            builder.Property(p => p.Number)
                .HasColumnName("contact_phone_number")
                .IsRequired()
                .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGHT);

            builder.Property(p => p.CastrationStatus)
                .IsRequired()
                .HasColumnName("castration_status_enum")
                .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);

            builder.Property(p => p.PetVaccines)
                .HasColumnName("pet_vaccienes")
                .HasColumnType("jsonb")
                .HasConversion(
                    v => ThrowWriteToDatabaseException(),
                    v => (JsonSerializer.Deserialize<List<PetVaccine>>(
                        v, JsonSerializerOptions.Default) ?? new List<PetVaccine>())
                    .Select(pv => pv.Name).ToArray());

            builder.Property(p => p.HelpStatus)
                .IsRequired()
                .HasColumnName("help_status_enum")
                .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);

            builder.Property(p => p.CardNumber)
                .HasColumnName("card_number_for_payment")
                .IsRequired(false)
                .HasMaxLength(DetailsForPayment.MAX_CARD_NUMBER_LENGHT);

            builder.Property(p => p.OtherDetails)
                .HasColumnName("other_payment_details")
                .IsRequired(false)
                .HasMaxLength(DetailsForPayment.MAX_DETAILS_LENGHT);

            // PaymentDetails can be null, so its properties CardNumber and OtherDetails set as not required
            // but if PaymentDetails is not null, then CardNumber is required. So there is a check constraint
            // that checks and throw an error if OtherDetails is not null and CardNumber is null
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_PaymentDetails_CardNumberRequiredIfOtherExists",
                    "other_payment_details IS NULL OR card_number_for_payment IS NOT NULL");
            });

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .HasConversion(new DateTimeToBinaryConverter());

            builder.Property(p => p.PositionNumber)
                .HasColumnName("pet_position")
                .IsRequired();

            builder.HasIndex(p => p.PositionNumber)
                .IsUnique();

            builder.Property(p => p.PetPhotos)
                .HasColumnName("pet_photos_paths")
                .HasColumnType("jsonb")
                .HasConversion(
                    x => ThrowWriteToDatabaseException(),
                    json => JsonSerializer.Deserialize<FileName[]>(
                        json, JsonSerializerOptions.Default) ?? Array.Empty<FileName>());

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("is_deleted");

        }

        private static string ThrowWriteToDatabaseException()
        {
            throw new NotSupportedException("Read-only context — write not supported.");
        }
    }
}
