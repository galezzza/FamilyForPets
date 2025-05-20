using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain.Species
{
    public class Breed : Entity<BreedId>
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        public Breed(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;
    }
}
