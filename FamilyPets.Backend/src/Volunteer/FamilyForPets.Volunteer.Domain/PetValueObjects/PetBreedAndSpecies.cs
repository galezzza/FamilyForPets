using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PetBreedAndSpecies : ValueObject
    {
        private PetBreedAndSpecies(Guid speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public Guid SpeciesId { get; }

        public Guid BreedId { get; }

        public static Result<PetBreedAndSpecies, Error> Create(Guid speciesId, Guid breedId)
        {
            if (speciesId == Guid.Empty)
                return Result.Failure<PetBreedAndSpecies, Error>(Errors.General.CannotBeEmpty("Species ID"));
            if (breedId == Guid.Empty)
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
