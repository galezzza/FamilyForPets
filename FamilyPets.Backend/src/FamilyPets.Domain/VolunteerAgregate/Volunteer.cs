using CSharpFunctionalExtensions;
using FamilyForPets.Domain.SharedValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate
{
    public class Volunteer : Entity<VolunteerId>
    {
        private List<Pet> _allPets = [];

        // for EF Core
        private Volunteer(VolunteerId id)
            : base(id)
        {
        }

        private Volunteer(
            FullName fullName,
            EmailAdress email,
            VolunteerDescription description,
            int experienceInYears,
            List<Pet> allPets,
            PhoneNumber phoneNumber,
            VolunteerSocialNetworksList socialNetworks,
            DetailsForPayment detailsForPayment)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            ExperienceInYears = experienceInYears;
            _allPets = allPets;
            PhoneNumber = phoneNumber;
            VolunteerSocialNetworks = socialNetworks;
            DetailsForPayment = detailsForPayment;
        }

        public FullName FullName { get; private set; } = default!;

        public EmailAdress Email { get; private set; } = default!;

        public VolunteerDescription Description { get; private set; } = VolunteerDescription.Empty(); // can be null

        public int ExperienceInYears { get; private set; }

        public IReadOnlyCollection<Pet> AllPets => _allPets.AsReadOnly();

        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public VolunteerSocialNetworksList VolunteerSocialNetworks { get; private set; } = default!;

        public DetailsForPayment DetailsForPayment { get; private set; } = default!;

        public Result<Volunteer, Error> Create(
            FullName fullName,
            EmailAdress email,
            VolunteerDescription description,
            int experienceInYears,
            List<Pet> allPets,
            PhoneNumber phoneNumber,
            List<SocialNetwork> socialNetworks,
            DetailsForPayment detailsForPayment)
        {
            if (experienceInYears < 0)
            {
                return Result.Failure<Volunteer, Error>(Errors.General.ValueIsInvalid("Volunteer Experience"));
            }

            return Result.Success<Volunteer, Error>(new Volunteer(
                fullName,
                email,
                description,
                experienceInYears,
                allPets,
                phoneNumber,
                VolunteerSocialNetworksList.Create(socialNetworks).Value,
                detailsForPayment));
        }

        public int GetNumeberOfPetsWithHelpNeeded() => AllPets.Where(p => p.HelpStatus == HelpStatus.HelpNeeded).Count();

        public int GetNumeberOfPetsWithHelpInProgress() => AllPets.Where(p => p.HelpStatus == HelpStatus.LookingForHome).Count();

        public int GetNumeberOfPetsWithFoundedHome() => AllPets.Where(p => p.HelpStatus == HelpStatus.HomeFounded).Count();

    }
}
