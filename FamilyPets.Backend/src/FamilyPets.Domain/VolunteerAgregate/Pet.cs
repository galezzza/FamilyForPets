using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using Microsoft.VisualBasic;

namespace FamilyForPets.Domain.VolunteerAgregate
{
    public class Pet : Entity<PetId>
    {
        public string Name { get; private set; } = default!;

        public string? Description { get; private set; }

        public PelageColor Color { get; private set; } = default!;

        public DateTime? DateOfBirth { get; private set; }

        public PetBreedAndSpecies PetBreed { get; private set; } = default!;

        public PetHealthDescription? PetHealthDescription { get; private set; }

        public Adress? PetCurrentAdress { get; private set; }

        public int? Weight { get; private set; }

        public int? Height { get; private set; }

        public PhoneNumber ContactPhoneNumber { get; private set; } = default!;

        public bool IsNeutered { get; private set; }

        public bool? IsVaccinated { get; private set; }

        public HelpStatus HelpStatus { get; private set; } = HelpStatus.HelpNeeded;

        public DetailsForPayment? PaymentDatails { get; private set; }

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
            PetHealthDescription = petHealthDescription;
            PetCurrentAdress = petCurrentAdress;
            Weight = weight;
            Height = height;
            ContactPhoneNumber = contactPhoneNumber;
            IsNeutered = isNeutered;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            PaymentDatails = paymentDatails;
            CreatedAt = DateTime.UtcNow;
        }

    }
}
