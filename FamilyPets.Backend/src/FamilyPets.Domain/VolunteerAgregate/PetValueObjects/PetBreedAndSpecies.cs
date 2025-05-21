using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;
using FamilyForPets.Domain.SpeciesAgregate;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PetBreedAndSpecies : ValueObject
    {
        private PetBreedAndSpecies(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public SpeciesId SpeciesId { get; }

        public BreedId BreedId { get; }

        public static Result<PetBreedAndSpecies, Error> Create(SpeciesId speciesId, BreedId breedId)
        {
            if (speciesId.Value == Guid.Empty)
                return Result.Failure<PetBreedAndSpecies, Error>(Errors.General.CannotBeEmpty("Species ID"));
            if (breedId.Value == Guid.Empty)
                return Result.Failure<PetBreedAndSpecies, Error>(Errors.General.CannotBeEmpty("Breed ID"));
            return Result.Success<PetBreedAndSpecies, Error>(
                new PetBreedAndSpecies(speciesId, breedId));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SpeciesId;
            yield return BreedId;
        }
    }
}
