using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Species;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PetBreedAndSpecies : ValueObject
    {
        public SpeciesId SpeciesId { get; }

        public BreedId BreedId { get; }

        private PetBreedAndSpecies(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<PetBreedAndSpecies> Create(SpeciesId speciesId, BreedId breedId)
        {
            if (speciesId.Value == Guid.Empty)
                return Result.Failure<PetBreedAndSpecies>("Species ID cannot be empty.");
            if (breedId.Value == Guid.Empty)
                return Result.Failure<PetBreedAndSpecies>("Breed ID cannot be empty.");
            return Result.Success(new PetBreedAndSpecies(speciesId, breedId));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SpeciesId;
            yield return BreedId;
        }
    }
}
