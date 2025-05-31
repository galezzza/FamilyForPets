using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.SharedValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public const int MAX_PHONE_NUMBER_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;
        public const string PHONE_NUMBER_PATTERN = @"^\+?\d{9,15}$";

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; } = default!;

        public static Result<PhoneNumber, Error> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return Result.Failure<PhoneNumber, Error>(Errors.General.CannotBeEmpty("Phone number"));

            if (!Regex.IsMatch(number, PHONE_NUMBER_PATTERN))
                return Result.Failure<PhoneNumber, Error>(Errors.General.ValueIsInvalid("Phone number"));

            return Result.Success<PhoneNumber, Error>(
                new PhoneNumber(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
