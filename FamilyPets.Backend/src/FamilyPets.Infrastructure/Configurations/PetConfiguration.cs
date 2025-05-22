using FamilyForPets.Domain;
using FamilyForPets.Domain.SpeciesAgregate;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Infrastructure.Configurations.Converters;
using FamilyForPets.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyForPets.Infrastructure.Configurations
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
                .IsRequired()
                .HasMaxLength(Pet.MAX_NAME_LENGHT);

            builder.Property(p => p.Description)
                .HasMaxLength(Pet.MAX_DESCRIPTION_LENGHT)
                .IsRequired(false);

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
                    .IsRequired()
                    .HasConversion(
                        breedId => breedId.Value,
                        value => BreedId.Create(value));
                pbb.Property(p => p.SpeciesId)
                    .HasColumnName("species_id")
                    .IsRequired()
                    .HasConversion(
                        speciesId => speciesId.Value,
                        value => SpeciesId.Create(value));
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

            builder.Property(p => p.Weight)
                .HasColumnName("weight")
                .IsRequired(false);

            builder.Property(p => p.Height)
                .HasColumnName("height")
                .IsRequired(false);

            builder.Property(p => p.ContactPhoneNumber)
                .HasColumnName("contact_phone_number")
                .IsRequired()
                .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGHT)
                .HasConversion(
                    phoneNumber => phoneNumber.Number,
                    number => PhoneNumber.Create(number).Value);

            builder.Property(p => p.IsNeutered)
                .HasColumnName("is_neutered")
                .IsRequired();

            builder.Property(p => p.IsVaccinated)
                .HasColumnName("is_vaccinated")
                .IsRequired(false);

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
        }

    }
}
