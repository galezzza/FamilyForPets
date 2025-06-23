using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.PetValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;

namespace FamilyForPets.Volunteers.Domain.Entities
{
    public class Volunteer : SoftDeletableEntity<VolunteerId>
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
            : base(id)
        {
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

        public override void SoftDelete()
        {
            base.SoftDelete();
            _allPets.ForEach(p => p.SoftDelete());
        }

        public override void Restore()
        {
            base.Restore();
            _allPets.ForEach(p => p.Restore());
        }

        public Result<PetId, Error> CreateNewPet(
            PetNickname name,
            PelageColor color,
            DateTime? dateOfBirth,
            PetBreedAndSpecies petBreed,
            PhoneNumber contactPhoneNumber,
            CastrationStatus castrationStatus,
            HelpStatus helpStatus)
        {
            // add new pet to the end
            PetPosition petPosition = PetPosition.Create(_allPets.Count + 1).Value;

            Result<Pet, Error> petResult = Pet.Create(
                name,
                color,
                dateOfBirth,
                petBreed,
                contactPhoneNumber,
                castrationStatus,
                helpStatus,
                DetailsForPayment,
                petPosition);

            if (petResult.IsFailure)
                return petResult.Error;

            _allPets.Add(petResult.Value);
            return Result.Success<PetId, Error>(petResult.Value.Id);
        }

        public UnitResult<Error> ChangePetPositionToTheVeryBegging(Pet pet)
        {
            PetPosition newPosition = PetPosition.Create(1).Value;
            UnitResult<Error> result = ChangePetPosition(pet, newPosition);
            if (result.IsFailure)
                return result.Error;

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> ChangePetPositionToTheVeryEnd(Pet pet)
        {
            PetPosition newPosition = PetPosition.Create(_allPets.Count).Value;
            UnitResult<Error> result = ChangePetPosition(pet, newPosition);
            if (result.IsFailure)
                return result.Error;

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> ChangePetPosition(Pet pet, PetPosition newPosition)
        {
            // new position == current position
            PetPosition petCurrentPosition = pet.PetPosition;
            if (petCurrentPosition == newPosition)
                return UnitResult.Success<Error>();

            // new position should not be greater than total number of pets
            if (newPosition.PositionNumber > _allPets.Count)
                return UnitResult.Failure<Error>(Errors.General.ValueIsInvalid(nameof(newPosition)));

            bool isPetPositionIncreasing = newPosition.PositionNumber 
                > petCurrentPosition.PositionNumber;

            int petCurrentIndex = petCurrentPosition.PositionNumber - 1;
            int newPositionIndex = newPosition.PositionNumber - 1;

            switch (isPetPositionIncreasing)
            {
                case true:
                    ChangeIntermediatesPetsPositions(
                        petCurrentIndex + 1, newPositionIndex, !isPetPositionIncreasing);
                    break;
                case false:
                    ChangeIntermediatesPetsPositions(
                        newPositionIndex, petCurrentIndex - 1, !isPetPositionIncreasing);
                    break;
            }

            pet.ChangePetPosition(newPosition);

            return UnitResult.Success<Error>();
        }

        private UnitResult<Error> ChangeIntermediatesPetsPositions(
            int fromIndex,
            int toIndex,
            bool isChangesIncreasing)
        {
            int positionChanger = isChangesIncreasing ? +1 : -1;

            for (int petIndex = fromIndex; petIndex <= toIndex; petIndex += 1)
            {
                Pet petToChangePosition = _allPets[petIndex];
                PetPosition oldPetPosition = petToChangePosition.PetPosition;

                int newPositionNumber = oldPetPosition.PositionNumber + positionChanger;
                Result<PetPosition, Error> newPetPositionResult = PetPosition.Create(newPositionNumber);
                if (newPetPositionResult.IsFailure)
                    throw new ArgumentException("volunteer have pets with incorrect positions");

                PetPosition newPetPosition = newPetPositionResult.Value;
                petToChangePosition.ChangePetPosition(newPetPosition);
            }

            return UnitResult.Success<Error>();
        }
    }
}
