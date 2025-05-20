using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Species
{

    public class SpeciesBreedsList : ValueObject
    {
        private List<Breed> _breeds = [];

        // for EF Core
        private SpeciesBreedsList()
        {
        }

        private SpeciesBreedsList(List<Breed> breeds)
        {
            _breeds = breeds;
        }

        public IReadOnlyCollection<Breed> Breeds => _breeds.AsReadOnly();

        public static Result<SpeciesBreedsList> Create(List<Breed> breeds)
        {
            return Result.Success(new SpeciesBreedsList(breeds));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _breeds;
        }
    }
}