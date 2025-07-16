using System.Drawing;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.Entities;

namespace FamilyForPets.Volunteers.Contracts.DTOs
{
    // class or record ???
    public class PetDTO
    {
        public Guid Id { get; init; } = default!;

        public Guid VolunteerId { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string? Description { get; init; }

        public Color PrimaryColor { get; init; }

        public Color? SecondaryColor { get; init; }

        public Color? TertiaryColor { get; init; }

        public DateTime? DateOfBirth { get; init; }

        public Guid SpeciesId { get; init; }

        public Guid BreedId { get; init; }

        public string? PetHealthDescription { get; init; }

        public string? HouseNumber { get; init; }

        public string? Street { get; init; }

        public string? City { get; init; }

        public string? Country { get; init; }

        public double? Weight { get; init; }

        public string? MassType { get; init; }

        public double? Height { get; init; }

        public string? LengthType { get; init; }

        public string Number { get; init; } = default!;

        public string CastrationStatus { get; init; } = default!;

        public string[] PetVaccines { get; init; } = [];

        public string? HelpStatus { get; init; } = default!;

        public string CardNumber { get; init; } = default!;

        public string? OtherDetails { get; init; }

        public DateTime CreatedAt { get; init; } = default!;

        public int PositionNumber { get; init; }

        public FileName[] PetPhotos { get; init; } = [];

        public bool IsDeleted { get; init; }

        public static PetDTO CreateFromEntity(Pet pet)
        {
            return new PetDTO
            {
                Id = pet.Id.Value,
                Name = pet.Name.Name,
                Description = pet.Description.Description,
                PrimaryColor = pet.Color.PrimaryColor,
                SecondaryColor = pet.Color.SecondaryColor,
                TertiaryColor = pet.Color.TertiaryColor,
                DateOfBirth = pet.DateOfBirth,
                SpeciesId = pet.PetBreed.SpeciesId,
                BreedId = pet.PetBreed.BreedId,
                PetHealthDescription = pet.PetHealthDescription.Description,
                HouseNumber = pet.PetCurrentAdress.HouseNumber,
                Street = pet.PetCurrentAdress.Street,
                City = pet.PetCurrentAdress.City,
                Country = pet.PetCurrentAdress.Country,
                Weight = pet.Weight.Value,
                MassType = pet.Weight.Type.Value,
                Height = pet.Height.Value,
                LengthType = pet.Height.Type.Value,
                Number = pet.ContactPhoneNumber.Number,
                CastrationStatus = pet.CastrationStatus.Value,
                PetVaccines = pet.PetVaccines.PetVaccines
                    .Select(v => v.Name)
                    .ToArray(),
                HelpStatus = pet.HelpStatus.Value,
                CardNumber = pet.PaymentDatails.CardNumber,
                OtherDetails = pet.PaymentDatails.OtherDetails,
                CreatedAt = pet.CreatedAt,
                PositionNumber = pet.PetPosition.PositionNumber,
                PetPhotos = pet.PetPhotos.FilePaths
                    .Select(f => f.Path)
                    .ToArray(),
                IsDeleted = pet.IsDeleted,
            };
        }

    }
}
