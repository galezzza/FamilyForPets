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
            VolunteerId id,
            FullName fullName,
            EmailAdress email,
            int experienceInYears,
            PhoneNumber phoneNumber,
            VolunteerSocialNetworksList volunteerSocialNetworks,
            DetailsForPayment detailsForPayment)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            ExperienceInYears = experienceInYears;
            PhoneNumber = phoneNumber;
            VolunteerSocialNetworks = volunteerSocialNetworks;
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

        public static Result<Volunteer, Error> Create(
            FullName fullName,
            EmailAdress email,
            int experienceInYears,
            PhoneNumber phoneNumber,
            DetailsForPayment detailsForPayment)
        {
            if (experienceInYears < 0)
            {
                return Result.Failure<Volunteer, Error>(Errors.General.ValueIsInvalid("Volunteer Experience"));
            }

            return Result.Success<Volunteer, Error>(new Volunteer(
                VolunteerId.New(),
                fullName,
                email,
                experienceInYears,
                phoneNumber,
                VolunteerSocialNetworksList.Create([]).Value,
                detailsForPayment));
        }

        public int GetNumeberOfPetsWithHelpNeeded() => AllPets.Where(p => p.HelpStatus == HelpStatus.HelpNeeded).Count();

        public int GetNumeberOfPetsWithHelpInProgress() => AllPets.Where(p => p.HelpStatus == HelpStatus.LookingForHome).Count();

        public int GetNumeberOfPetsWithFoundedHome() => AllPets.Where(p => p.HelpStatus == HelpStatus.HomeFounded).Count();

        public UnitResult<Error> UpdateSocialNetworks(VolunteerSocialNetworksList socialNetworks)
        {
            VolunteerSocialNetworks = socialNetworks;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> UpdateDetailsForPayment(DetailsForPayment detailsForPayment)
        {
            DetailsForPayment = detailsForPayment;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> UpdateMainInfo(FullName fullName, VolunteerDescription description)
        {
            FullName = fullName;
            Description = description;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> UpdateContactData(PhoneNumber phoneNumber, EmailAdress emailAdress)
        {
            PhoneNumber = phoneNumber;
            Email = emailAdress;
            return UnitResult.Success<Error>();
        }
    }
}
