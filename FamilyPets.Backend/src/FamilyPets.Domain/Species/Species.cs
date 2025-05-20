using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain.Species
{
    public class Species : Entity<SpeciesId>
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        // empty constructor for EF Core
        private Species()
        {
        }

        public Species(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;

        public SpeciesBreedsList SpeciesBreeds { get; private set; } = default!;

    }
}
