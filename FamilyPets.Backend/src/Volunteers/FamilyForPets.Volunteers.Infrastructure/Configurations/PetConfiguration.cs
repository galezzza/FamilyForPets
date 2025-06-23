using System.Text.Json;
using FamilyForPets.Core.Configurations.Converters;
using FamilyForPets.Core.DTOs;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.PetValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyForPets.Volunteers.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value));

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasConversion(
                    nickname => nickname.Name,
                    nickname => PetNickname.Create(nickname).Value)
                .HasMaxLength(PetNickname.MAX_NAME_LENGHT);

            builder.Property(p => p.Description)
                .HasColumnName("pet_description")
                .IsRequired(false)
                .HasConversion(
                    description => description.Description,
                    description => PetDescription.Create(description).Value)
                .HasMaxLength(PetDescription.MAX_DESCRIPTION_LENGHT);

            builder.ComplexProperty(p => p.Color, cb =>
            {
                cb.Property(pc => pc.PrimaryColor)
                    .HasColumnName("primary_color")
                    .IsRequired()
                    .HasConversion<ColorToInt32Converter>();

                cb.Property(pc => pc.SecondaryColor)
                    .HasColumnName("secondary_color")
                    .IsRequired(false)
                    .HasConversion<ColorToInt32Converter>();

                cb.Property(pc => pc.TertiaryColor)
                    .HasColumnName("tertiary_color")
                    .IsRequired(false)
                    .HasConversion<ColorToInt32Converter>();
            });

            builder.Property(p => p.DateOfBirth)
                .HasColumnName("date_of_birth")
                .IsRequired(false)
                .HasConversion(new DateTimeToBinaryConverter());

            builder.ComplexProperty(p => p.PetBreed, pbb => {
                pbb.Property(p => p.BreedId)
                    .HasColumnName("breed_id")
                    .IsRequired();
                pbb.Property(p => p.SpeciesId)
                    .HasColumnName("species_id")
                    .IsRequired();
            });

            builder.Property(p => p.PetHealthDescription)
                .HasColumnName("pet_health_description")
                .IsRequired(false)
                .HasConversion(
                    healthDescription => healthDescription.Description,
                    description => PetHealthDescription.Create(description).Value)
                .HasMaxLength(PetHealthDescription.MAX_DESCRIPTION_LENGHT);

            builder.ComplexProperty(p => p.PetCurrentAdress, ab =>
            {
                ab.Property(a => a.Country)
                    .HasColumnName("country")
                    .IsRequired(false)
                    .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);
                ab.Property(a => a.City)
                    .HasColumnName("city")
                    .IsRequired(false)
                    .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);
                ab.Property(a => a.Street)
                    .HasColumnName("street")
                    .IsRequired(false)
                    .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);
                ab.Property(a => a.HouseNumber)
                    .HasColumnName("house_number")
                    .IsRequired(false)
                    .HasMaxLength(Adress.MAX_ADRESS_TEXT_LENGHT);
            });

            builder.ComplexProperty(p => p.Weight, wb =>
            {
                wb.Property(m => m.Value)
                    .HasColumnName("weight");

                wb.ComplexProperty(m => m.Type, mtb =>
                {
                    mtb.Property(s => s.Value)
                        .HasColumnName("mass_type_enum")
                        .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);
                });
            });

            // Weight can be null, so its properties Value and MassType set as not required
            // but if Weight is not null, then Value and MassType are required.
            // So there is a check constraint
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Weight_BothOrNone",
                    "(weight IS NULL AND mass_type_enum IS NULL) OR (weight IS NOT NULL AND mass_type_enum IS NOT NULL)");
            });

            builder.ComplexProperty(p => p.Height, hb =>
            {
                hb.Property(h => h.Value)
                    .HasColumnName("height");

                hb.ComplexProperty(h => h.Type, ltb =>
                {
                    ltb.Property(s => s.Value)
                        .HasColumnName("length_type_enum")
                        .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);
                });
            });

            // Height can be null, so its properties Value and LengthType set as not required
            // but if Height is not null, then Value and LengthType are required.
            // So there is a check constraint
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Height_BothOrNone",
                    "(height IS NULL AND length_type_enum IS NULL) OR (height IS NOT NULL AND length_type_enum IS NOT NULL)");
            });

            builder.Property(p => p.ContactPhoneNumber)
                .HasColumnName("contact_phone_number")
                .IsRequired()
                .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGHT)
                .HasConversion(
                    phoneNumber => phoneNumber.Number,
                    number => PhoneNumber.Create(number).Value);

            builder.ComplexProperty(p => p.CastrationStatus, csb =>
            {
                csb.IsRequired();
                csb.Property(s => s.Value)
                    .HasColumnName("castration_status_enum")
                    .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);
            });

            builder.OwnsOne(p => p.PetVaccines, pb =>
            {
                pb.ToJson("pet_vaccienes");
                pb.OwnsMany(pvb => pvb.PetVaccines, pv =>
                {
                    pv.Property(sn => sn.Name)
                        .IsRequired(false)
                        .HasColumnName("pet_vacciene")
                        .HasMaxLength(PetVaccine.MAX_NAME_LENGHT);
                });
            });

            builder.ComplexProperty(p => p.HelpStatus, hsb =>
            {
                hsb.IsRequired();
                hsb.Property(s => s.Value)
                    .HasColumnName("help_status_enum")
                    .HasMaxLength(ProjectConstants.MAX_LOW_TEXT_LENGHT);
            });

            builder.ComplexProperty(p => p.PaymentDatails, pdb =>
            {
                pdb.Property(pd => pd.CardNumber)
                    .HasColumnName("card_number_for_payment")
                    .IsRequired(false)
                    .HasMaxLength(DetailsForPayment.MAX_CARD_NUMBER_LENGHT);

                pdb.Property(pd => pd.OtherDetails)
                    .HasColumnName("other_payment_details")
                    .IsRequired(false)
                    .HasMaxLength(DetailsForPayment.MAX_DETAILS_LENGHT);
            });

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

            builder.Property(p => p.PetPosition)
                .HasColumnName("pet_position")
                .IsRequired()
                .HasConversion(
                    petPosition => petPosition.PositionNumber,
                    position => PetPosition.Create(position).Value);

            builder.HasIndex(p => p.PetPosition)
                .IsUnique();

            builder.OwnsOne(p => p.PetPhotos, pb =>
            {
                pb.ToJson("pet_photos_paths");
                pb.OwnsMany(fpb => fpb.FilePaths, fp =>
                {
                    fp.Property(sn => sn.Path)
                        .IsRequired()
                        .HasColumnName("photo_path")
                        .HasConversion(
                            x => JsonSerializer.Serialize(x, JsonSerializerOptions.Default),
                            json => JsonSerializer.Deserialize<FileName>(json, JsonSerializerOptions.Default));
                });
            });
        }

    }
}
