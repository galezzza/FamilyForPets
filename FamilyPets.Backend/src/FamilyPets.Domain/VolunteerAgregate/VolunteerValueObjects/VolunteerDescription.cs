using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects
{
    public class VolunteerDescription : ValueObject
    {
        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        private VolunteerDescription(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static VolunteerDescription Empty() => new VolunteerDescription(string.Empty);

        public static Result<VolunteerDescription, Error> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Success<VolunteerDescription, Error>(Empty());
            return Result.Success<VolunteerDescription, Error>(
                new VolunteerDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}