using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;

namespace FamilyForPets.Domain.VolunteerAgregate
{
    public class Volunteer : Entity<VolunteerId>
    {
        public FullName FullName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string? Description { get; private set; }

        public int ExperienceInYears { get; private set; }

        private List<Pet> _allPets = [];

        public IReadOnlyCollection<Pet> AllPets => _allPets.AsReadOnly();

        public string PhoneNumber { get; private set; } = default!;

        private List<SocialNetwork> _socialNetworks = [];

        public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks.AsReadOnly();

        public DetailsForPayment DetailsForPayment { get; private set; } = default!;

        public List<Pet> PetsHelpNeeded => GetPetsHelpNeeded();

        public List<Pet> PetsHomeFounded => GetPetsHomeFounded();

        public List<Pet> PetsHelpInProgress => GetPetsHelpInProgress();

        public Volunteer(
            FullName fullName,
            string email,
            string? description,
            int experienceInYears,
            List<Pet> allPets,
            string phoneNumber,
            List<SocialNetwork> socialNetworks,
            DetailsForPayment detailsForPayment)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            _allPets = allPets;
            PhoneNumber = phoneNumber;
            _socialNetworks = socialNetworks;
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
    }
}
