using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;

namespace FamilyForPets.Domain.VolunteerAgregate
{
    public class Volunteer : Entity<VolunteerId>
    {
        public const int MAX_EMAIL_LENGHT = ProjectConstants.MAX_MEDIUM_TEXT_LENGHT;

        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        public FullName FullName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string? Description { get; private set; }

        public int ExperienceInYears { get; private set; }

        private List<Pet> _allPets = [];

        public IReadOnlyCollection<Pet> AllPets => _allPets.AsReadOnly();

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public VolunteerSocialNetworksList VolunteerSocialNetworks { get; private set; } = default!;

        public DetailsForPayment DetailsForPayment { get; private set; } = default!;

        // public List<Pet> PetsHelpNeeded => GetPetsHelpNeeded();

        // public List<Pet> PetsHomeFounded => GetPetsHomeFounded();

        // public List<Pet> PetsHelpInProgress => GetPetsHelpInProgress();

        // because Volunteer has those 3 properties of type List<Pet> that refer to relavent methods
        // EF Core have indentified that as "multiple relationships between Pet and Volunteer"
        // and created three shadow foreign keys on those properties in database thats are not correct

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

        public List<Pet> GetPetsHelpNeeded()
        {
            return _allPets
            .Where(pet => pet.HelpStatus == HelpStatus.HelpNeeded)
            .ToList();
        }

        public List<Pet> GetPetsHelpInProgress()
        {
            return _allPets
            .Where(pet => pet.HelpStatus == HelpStatus.LookingForHome)
            .ToList();
        }

        public List<Pet> GetPetsHomeFounded()
        {
            return _allPets
            .Where(pet => pet.HelpStatus == HelpStatus.HomeFounded)
            .ToList();
        }

        // for EF Core
        private Volunteer(VolunteerId id)
            : base(id)
        {
        }

    }
}
