using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.SharedValueObjects
{
    public class EmailAdress : ValueObject
    {
        public const int MAX_DESCRIPTION_LENGHT = ProjectConstants.MAX_MEDIUM_TEXT_LENGHT;

        private EmailAdress(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static EmailAdress Empty() => new EmailAdress(string.Empty);

        public static Result<EmailAdress, Error> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<EmailAdress, Error>(Errors.General.CannotBeEmpty("Email adress"));
            return Result.Success<EmailAdress, Error>(
                new EmailAdress(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}