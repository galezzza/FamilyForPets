using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Species.Domain
{
    public class Breed : Entity<BreedId>
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        // empty constructor for EF Core
        private Breed()
        {
        }

        public Breed(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;
    }
}
