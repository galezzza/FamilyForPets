using FamilyForPets.Core.DTOs;
using FamilyForPets.Volunteers.Contracts.DTOs;
using FamilyForPets.Volunteers.Domain.Entities;

namespace FamilyForPets.Volunteers.Contracts.Responses
{
    public class VolunteerDTO
    {
        public Guid Id { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string? Surname { get; init; }

        public string? AdditionalName { get; init; }

        public string Email { get; init; } = default!;

        public string? Description { get; init; }

        public int ExperienceInYears { get; init; }

        public string PhoneNumber { get; init; } = default!;

        public string CardNumber { get; init; } = default!;

        public string? OtherDetails { get; init; }

        public SocialNetworkDTO[] SocialNetworks { get; init; } = [];

        public Guid[] Pets { get; init; } = [];

        public bool IsDeleted { get; init; }

        public static VolunteerDTO CreateFromEntity(Volunteer volunteer)
        {
            return new VolunteerDTO
            {
                Id = volunteer.Id.Value,
                Name = volunteer.FullName.Name,
                Surname = volunteer.FullName.Surname,
                AdditionalName = volunteer.FullName.AdditionalName,
                Email = volunteer.Email.Email,
                Description = volunteer.Description.Description,
                ExperienceInYears = volunteer.ExperienceInYears,
                PhoneNumber = volunteer.PhoneNumber.Number,
                CardNumber = volunteer.DetailsForPayment.CardNumber,
                OtherDetails = volunteer.DetailsForPayment.OtherDetails,
                SocialNetworks = volunteer.VolunteerSocialNetworks.SocialNetworks
                        .Select(sn => new SocialNetworkDTO(sn.Url, sn.Name))
                        .ToArray(),
                Pets = volunteer.AllPets
                    .Select(p => PetDTO.CreateFromEntity(p).Id)
                    .ToArray(),
                IsDeleted = volunteer.IsDeleted,
            };
        }

        public VolunteerDTO AppendPets(Guid[] ids)
        {
            return new VolunteerDTO
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                AdditionalName = this.AdditionalName,
                Email = this.Email,
                Description = this.Description,
                ExperienceInYears = this.ExperienceInYears,
                PhoneNumber = this.PhoneNumber,
                CardNumber = this.CardNumber,
                OtherDetails = this.OtherDetails,
                SocialNetworks = this.SocialNetworks,
                IsDeleted = this.IsDeleted,
                Pets = ids,
            };
        }
    }
}
