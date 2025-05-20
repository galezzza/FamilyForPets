using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain
{
    public class PhoneNumber : ValueObject
    {
        public const int MAX_PHONE_NUMBER_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; } = default!;

        public static Result<PhoneNumber> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return Result.Failure<PhoneNumber>("Phone number cannot be empty.");
            return Result.Success(new PhoneNumber(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
