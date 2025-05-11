using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; } = default!;
        private PhoneNumber(string number)
        {
            Number = number;
        }
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
