using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PetHealthDescription : ValueObject
    {
        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        private PetHealthDescription(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static PetHealthDescription Empty() => new PetHealthDescription(string.Empty);

        public static Result<PetHealthDescription, Error> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<PetHealthDescription, Error>(Errors.General.CannotBeEmpty("Description"));
            return Result.Success<PetHealthDescription, Error>(
                new PetHealthDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}
