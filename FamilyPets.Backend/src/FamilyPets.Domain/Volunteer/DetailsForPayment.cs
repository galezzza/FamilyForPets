using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Volunteer
{
    public class DetailsForPayment : ValueObject
    {
        public string CardNumber { get; private set; } = default!;

        public string OtherDetails { get; private set; } = default!;

        private DetailsForPayment(string cardNumber, string otherDetails)
        {
            CardNumber = cardNumber;
            OtherDetails = otherDetails;
        }

        public static Result<DetailsForPayment> Create(string cardNumber, string otherDetails)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return Result.Failure<DetailsForPayment>("Card number cannot be empty.");
            if (string.IsNullOrWhiteSpace(otherDetails))
                return Result.Failure<DetailsForPayment>("Other details cannot be empty.");
            return Result.Success(new DetailsForPayment(cardNumber, otherDetails));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CardNumber;
        }
    }
}
