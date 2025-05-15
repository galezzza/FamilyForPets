using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Species
{
    public class Breed : Entity<BreedId>
    {
        public string Name { get; private set; } = default!;

        public Breed(string name)
        {
            Name = name;
        }
    }
}
