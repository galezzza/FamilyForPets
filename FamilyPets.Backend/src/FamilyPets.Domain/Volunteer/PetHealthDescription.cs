using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Volunteer
{
    public class PetHealthDescription : ValueObject
    {
        public string Description { get; }

        private PetHealthDescription(string description)
        {
            Description = description;
        }

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
