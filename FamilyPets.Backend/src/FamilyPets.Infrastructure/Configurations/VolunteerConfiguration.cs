using System.Threading.Tasks.Dataflow;
using FamilyForPets.Domain;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyForPets.Infrastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value));

            builder.ComplexProperty(v => v.FullName, vb =>
            {
                vb.Property(fn => fn.Name)
                    .HasColumnName("first_name")
                    .IsRequired()
                    .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);
                vb.Property(fn => fn.Surname)
                    .HasColumnName("last_name")
                    .IsRequired()
                    .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);
                vb.Property(fn => fn.AdditionalName)
                    .HasColumnName("additional_name")
                    .IsRequired(false)
                    .HasMaxLength(FullName.MAX_NAME_TEXT_LENGHT);
            });

            builder.Property(v => v.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(Volunteer.MAX_EMAIL_LENGHT);

            builder.Property(v => v.Description)
                .HasColumnName("description")
                .IsRequired(false)
                .HasMaxLength(Volunteer.MAX_DESCRIPTION_LENGHT);

            builder.Property(v => v.ExperienceInYears)
                .HasColumnName("experience_in_years")
                .IsRequired();

            builder.Property(v => v.PhoneNumber)
               .HasColumnName("phone_number")
               .IsRequired()
               .HasMaxLength(PhoneNumber.MAX_PHONE_NUMBER_LENGHT)
               .HasConversion(
                   phoneNumber => phoneNumber.Number,
                   number => PhoneNumber.Create(number).Value);

            builder.ComplexProperty(v => v.DetailsForPayment, pdb =>
            {
                pdb.Property(pd => pd.CardNumber)
                    .HasColumnName("card_number_for_payment")
                    .IsRequired()
                    .HasMaxLength(DetailsForPayment.MAX_CARD_NUMBER_LENGHT);

                pdb.Property(pd => pd.OtherDetails)
                    .HasColumnName("other_payment_details")
                    .IsRequired(false)
                    .HasMaxLength(DetailsForPayment.MAX_DETAILS_LENGHT);
            });

            builder.HasMany(v => v.AllPets)
                .WithOne()
                .HasForeignKey("volunteer_id")
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.OwnsOne(v => v.VolunteerSocialNetworks, vb =>
            {
                vb.ToJson("volunteer_social_newtworks");
                vb.OwnsMany(vsnb => vsnb.SocialNetworks, snb =>
                {
                    snb.Property(sn => sn.Name)
                        .IsRequired()
                        .HasColumnName("social_network_name")
                        .HasMaxLength(SocialNetwork.MAX_NAME_LENGHT);

                    snb.Property(sn => sn.Url)
                        .IsRequired()
                        .HasColumnName("social_network_name")
                        .HasMaxLength(SocialNetwork.MAX_URL_LENGHT);

                });
            });
        }
    }
}
