using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Volunteer
{
    public class PetBreedAndSpecies : ValueObject
    {
        public Guid SpeciesId { get; }
        
        public Guid BreedId { get; }

        private PetBreedAndSpecies(Guid speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<PetBreedAndSpecies> Create(Guid speciesId, Guid breedId)
        {
            if (speciesId == Guid.Empty)
                return Result.Failure<PetBreedAndSpecies>("Species ID cannot be empty.");
            if (breedId == Guid.Empty)
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
