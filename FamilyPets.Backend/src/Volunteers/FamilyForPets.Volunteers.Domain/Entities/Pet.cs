using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.PetValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;

namespace FamilyForPets.Volunteers.Domain.Entities
{
    public class Pet : SoftDeletableEntity<PetId>
    {
        // empty constructor for EF Core
        private Pet(PetId id)
            : base(id)
        {
        }

        private Pet(
            PetId id,
            PetNickname name,
            PelageColor color,
            DateTime? dateOfBirth,
            PetBreedAndSpecies petBreed,
            PhoneNumber contactPhoneNumber,
            CastrationStatus castrationStatus,
            HelpStatus helpStatus,
            PetPosition petPosition)
            : base(id)
        {
            Name = name;
            Color = color;
            DateOfBirth = dateOfBirth;
            PetBreed = petBreed;
            ContactPhoneNumber = contactPhoneNumber;
            CastrationStatus = castrationStatus;
            HelpStatus = helpStatus;
            PetPosition = petPosition;
            CreatedAt = DateTime.UtcNow;
        }

        public PetNickname Name { get; private set; } = default!;

        public PetDescription Description { get; private set; } = PetDescription.Empty(); // can br null

        public PelageColor Color { get; private set; } = default!;

        public DateTime? DateOfBirth { get; private set; }

        public PetBreedAndSpecies PetBreed { get; private set; } = default!;

        public PetHealthDescription PetHealthDescription { get; private set; } = PetHealthDescription.Empty(); // can be null

        public Adress PetCurrentAdress { get; private set; } = Adress.Empty(); // can be null

        public Mass Weight { get; private set; } = Mass.Empty(); // can be null

        public Length Height { get; private set; } = Length.Empty(); // can be null

        public PhoneNumber ContactPhoneNumber { get; private set; } = default!;

        public CastrationStatus CastrationStatus { get; private set; }

        public PetVaccinesList PetVaccines { get; private set; } = PetVaccinesList.Empty(); // can be null

        public HelpStatus HelpStatus { get; private set; } = HelpStatus.HelpNeeded;

        public DetailsForPayment PaymentDatails { get; private set; } = DetailsForPayment.Empty(); // can be null

        public DateTime CreatedAt { get; private set; } = default!;

        public PetPosition PetPosition { get; private set; } = default!;

        public FilePathsList PetPhotos { get; private set; } = default!;

        public static Result<Pet, Error> Create(
            PetNickname name,
            //PetDescription description,
            PelageColor color,
            DateTime? dateOfBirth,
            PetBreedAndSpecies petBreed,
            //PetHealthDescription petHealthDescription,
            //Adress petCurrentAdress,
            //Mass weight,
            //Length height,
            PhoneNumber contactPhoneNumber,
            CastrationStatus castrationStatus,
            //PetVaccinesList petVaccinesList,
            HelpStatus helpStatus,
            DetailsForPayment paymentDatails,
            PetPosition petPosition)
        {
            return Result.Success<Pet, Error>(new Pet(
                PetId.New(),
                name,
                color,
                dateOfBirth,
                petBreed,
                contactPhoneNumber,
                castrationStatus,
                helpStatus,
                petPosition));
        }

        internal UnitResult<Error> ChangePetPosition(PetPosition newPetPosition)
        {
            PetPosition = newPetPosition;
            return UnitResult.Success<Error>();
        }
    }
}
