using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PetHealthDescription : ValueObject
    {
        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        public string Description { get; }

        private PetHealthDescription(string description)
        {
            Description = description;
        }

        public static PetHealthDescription Empty() => new PetHealthDescription(string.Empty);

        public static Result<PetHealthDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<PetHealthDescription>("Description cannot be empty.");
            return Result.Success(new PetHealthDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}
