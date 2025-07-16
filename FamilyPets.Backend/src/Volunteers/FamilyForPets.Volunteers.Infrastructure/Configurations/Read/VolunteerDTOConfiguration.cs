using System.Text.Json;
using FamilyForPets.Core.DTOs;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Contracts.Responses;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyForPets.Volunteers.Infrastructure.Configurations.Write
{
    public class VolunteerDTOConfiguration : IEntityTypeConfiguration<VolunteerDTO>
    {
        public void Configure(EntityTypeBuilder<VolunteerDTO> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id);

            builder.Property(fn => fn.Name)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);

            builder.Property(fn => fn.Surname)
                .HasColumnName("last_name")
                .IsRequired(false)
                .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);

            builder.Property(fn => fn.AdditionalName)
                .HasColumnName("additional_name")
                .IsRequired(false)
                .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(EmailAdress.MAX_EMAIL_ADDRESS_LENGTH);

            builder.Property(p => p.Description)
                .HasColumnName("volunteer_description")
                .IsRequired(false)
                .HasMaxLength(VolunteerDescription.MAX_DESCRIPTION_LENGHT);

            builder.Property(v => v.ExperienceInYears)
                .HasColumnName("experience_in_years")
                .IsRequired();

            builder.Property(v => v.PhoneNumber)
               .HasColumnName("phone_number")
               .IsRequired()
               .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGHT);

            builder.Property(pd => pd.CardNumber)
                    .HasColumnName("card_number_for_payment")
                    .IsRequired()
                    .HasMaxLength(DetailsForPayment.MAX_CARD_NUMBER_LENGHT);

            builder.Property(pd => pd.OtherDetails)
                .HasColumnName("other_payment_details")
                .IsRequired(false)
                .HasMaxLength(DetailsForPayment.MAX_DETAILS_LENGHT);

            builder.Property(v => v.Pets)
                .HasColumnName("all_pets")
                .HasConversion(
                    v => JsonSerializer.Serialize(
                        v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<Guid[]>(
                        v, JsonSerializerOptions.Default) ?? Array.Empty<Guid>())
                .HasColumnType("jsonb");

            builder.Property(p => p.SocialNetworks)
                .HasColumnName("volunteer_social_newtworks")
                .HasConversion(
                    v => JsonSerializer.Serialize(
                        v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<SocialNetworkDTO[]>(
                        v, JsonSerializerOptions.Default) ?? Array.Empty<SocialNetworkDTO>())
                .HasColumnType("jsonb");

        }
    }
}
