using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class EmailAdress : ValueObject
    {
        public const int MAX_EMAIL_ADDRESS_LENGTH = ProjectConstants.MAX_MEDIUM_TEXT_LENGHT;
        private const string EMAIL_PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        private EmailAdress(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public static EmailAdress Empty() => new EmailAdress(string.Empty);

        public static Result<EmailAdress, Error> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<EmailAdress, Error>(Errors.General.CannotBeEmpty("Email adress"));

            if(!Regex.IsMatch(email, EMAIL_PATTERN))
                return Result.Failure<EmailAdress, Error>(Errors.General.ValueIsInvalid("Email adress"));

            return Result.Success<EmailAdress, Error>(
                new EmailAdress(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
    }
}