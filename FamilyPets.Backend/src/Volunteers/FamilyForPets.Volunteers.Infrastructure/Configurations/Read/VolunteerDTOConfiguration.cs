using System.Text.Json;
using FamilyForPets.Core.DTOs;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Contracts.Responses;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyForPets.Volunteers.Infrastructure.Configurations.Read
{
    public class VolunteerDTOConfiguration : IEntityTypeConfiguration<VolunteerDTO>
    {
        public void Configure(EntityTypeBuilder<VolunteerDTO> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id);

            builder.Property(v => v.Name)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);

            builder.Property(v => v.Surname)
                .HasColumnName("last_name")
                .IsRequired(false)
                .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);

            builder.Property(v => v.AdditionalName)
                .HasColumnName("additional_name")
                .IsRequired(false)
                .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);

            builder.Property(v => v.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(EmailAdress.MAX_EMAIL_ADDRESS_LENGTH);

            builder.Property(v => v.Description)
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

            builder.Property(v => v.CardNumber)
                    .HasColumnName("card_number_for_payment")
                    .IsRequired()
                    .HasMaxLength(DetailsForPayment.MAX_CARD_NUMBER_LENGHT);

            builder.Property(v => v.OtherDetails)
                .HasColumnName("other_payment_details")
                .IsRequired(false)
                .HasMaxLength(DetailsForPayment.MAX_DETAILS_LENGHT);

            builder.Property(v => v.SocialNetworks)
                .HasColumnName("volunteer_social_networks")
                .HasConversion(
                    v => ThrowWriteToDatabaseException(),
                    v => (
                            JsonSerializer.Deserialize<List<SocialNetwork>>(
                                v, JsonSerializerOptions.Default)
                            ?? new List<SocialNetwork>()
                        ).Select(sn => new SocialNetworkDTO(sn.Url, sn.Name))
                    .ToArray())
                .HasColumnType("jsonb");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("is_deleted");

            builder.Ignore(v => v.Pets);
        }

        private static string ThrowWriteToDatabaseException()
        {
            throw new NotSupportedException("Read-only context — write not supported.");
        }
    }
}
