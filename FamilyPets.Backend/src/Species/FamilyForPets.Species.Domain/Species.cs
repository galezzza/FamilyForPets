using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Species.Domain
{
    public class Species : Entity<SpeciesId>
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private List<Breed> _breeds = [];

        // empty constructor for EF Core
        private Species(SpeciesId id)
            : base(id)
        {
        }

        public Species(string name, List<Breed> breeds)
        {
            Name = name;
            _breeds = breeds;
        }

        public string Name { get; private set; } = default!;

        public IReadOnlyCollection<Breed> Breeds => _breeds.AsReadOnly();

    }
}
