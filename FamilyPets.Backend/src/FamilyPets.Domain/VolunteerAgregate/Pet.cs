using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;

namespace FamilyForPets.Domain.VolunteerAgregate
{
    public class Pet : Entity<PetId>
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        public string Name { get; private set; } = default!;

        public string? Description { get; private set; }

        public PelageColor Color { get; private set; } = default!;

        public DateTime? DateOfBirth { get; private set; }

        public PetBreedAndSpecies PetBreed { get; private set; } = default!;

        public PetHealthDescription PetHealthDescription { get; private set; } = PetHealthDescription.Empty(); // can be null

        public Adress PetCurrentAdress { get; private set; } = Adress.Empty(); // can be null

        public int? Weight { get; private set; }

        public int? Height { get; private set; }

        public PhoneNumber ContactPhoneNumber { get; private set; } = default!;

        public bool IsNeutered { get; private set; }

        public bool? IsVaccinated { get; private set; }

        public HelpStatus HelpStatus { get; private set; } = HelpStatus.HelpNeeded;

        public DetailsForPayment PaymentDatails { get; private set; } = DetailsForPayment.Empty(); // can be null

        public DateTime CreatedAt { get; private set; } = default!;

        public Pet(
            string name,
            string? description,
            PelageColor color,
            DateTime? dateOfBirth,
            PetBreedAndSpecies petBreed,
            PetHealthDescription? petHealthDescription,
            Adress? petCurrentAdress,
            int? weight,
            int? height,
            PhoneNumber contactPhoneNumber,
            bool isNeutered,
            bool? isVaccinated,
            HelpStatus helpStatus,
            DetailsForPayment? paymentDatails)
        {
            Name = name;
            Description = description;
            Color = color;
            DateOfBirth = dateOfBirth;
            PetBreed = petBreed;
            PetHealthDescription = InitializePetHealthDescription(petHealthDescription);
            PetCurrentAdress = InitializePetCurrentAdress(petCurrentAdress);
            Weight = weight;
            Height = height;
            ContactPhoneNumber = contactPhoneNumber;
            IsNeutered = isNeutered;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            PaymentDatails = InitializePaymentDatails(paymentDatails);
            CreatedAt = DateTime.UtcNow;
        }

        private PetHealthDescription InitializePetHealthDescription(PetHealthDescription? petHealthDescription)
        {
            if (petHealthDescription == null)
                return PetHealthDescription.Empty();
            return petHealthDescription;
        }

        private Adress InitializePetCurrentAdress(Adress? petCurrentAdress)
        {
            if (petCurrentAdress == null)
                return Adress.Empty();
            return petCurrentAdress;
        }

        private DetailsForPayment InitializePaymentDatails(DetailsForPayment? paymentDatails)
        {
            if (paymentDatails == null)
                return DetailsForPayment.Empty();
            return paymentDatails;
        }

        // empty constructor for EF Core
        private Pet(PetId id)
            : base(id)
        {
        }

    }
}
