using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.SharedValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public const int MAX_PHONE_NUMBER_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; } = default!;

        public static Result<PhoneNumber, Error> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return Result.Failure<PhoneNumber, Error>(Errors.General.CannotBeEmpty("Phone number"));
            return Result.Success<PhoneNumber, Error>(
                new PhoneNumber(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
