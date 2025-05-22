using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate
{
    public class Volunteer : Entity<VolunteerId>
    {
        public const int MAX_EMAIL_LENGHT = ProjectConstants.MAX_MEDIUM_TEXT_LENGHT;

        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        private List<Pet> _allPets = [];

        // for EF Core
        private Volunteer(VolunteerId id)
            : base(id)
        {
        }

        public Volunteer(
            FullName fullName,
            string email,
            string? description,
            int experienceInYears,
            List<Pet> allPets,
            PhoneNumber phoneNumber,
            List<SocialNetwork> socialNetworks,
            DetailsForPayment detailsForPayment)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            _allPets = allPets;
            PhoneNumber = phoneNumber;
            VolunteerSocialNetworks = VolunteerSocialNetworksList.Create(socialNetworks).Value;
            DetailsForPayment = detailsForPayment;
        }

        public FullName FullName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string? Description { get; private set; }

        public int ExperienceInYears { get; private set; }

        public IReadOnlyCollection<Pet> AllPets => _allPets.AsReadOnly();

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public VolunteerSocialNetworksList VolunteerSocialNetworks { get; private set; } = default!;

        public DetailsForPayment DetailsForPayment { get; private set; } = default!;


        public int GetNumeberOfPetsWithHelpNeeded() => AllPets.Where(p => p.HelpStatus == HelpStatus.HelpNeeded).Count();

        public int GetNumeberOfPetsWithHelpInProgress() => AllPets.Where(p => p.HelpStatus == HelpStatus.LookingForHome).Count();

        public int GetNumeberOfPetsWithFoundedHome() => AllPets.Where(p => p.HelpStatus == HelpStatus.HomeFounded).Count();
    }
}
