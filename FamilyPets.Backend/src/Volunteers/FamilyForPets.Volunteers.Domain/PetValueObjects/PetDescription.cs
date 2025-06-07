using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PetDescription : ValueObject
    {
        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        private PetDescription(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static PetDescription Empty() => new PetDescription(string.Empty);

        public static Result<PetDescription, Error> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<PetDescription, Error>(Errors.General.CannotBeEmpty("Pet description"));
            return Result.Success<PetDescription, Error>(
                new PetDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}