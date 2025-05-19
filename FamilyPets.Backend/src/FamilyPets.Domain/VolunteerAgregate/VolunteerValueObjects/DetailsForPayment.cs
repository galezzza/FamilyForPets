using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects
{
    public class DetailsForPayment : ValueObject
    {
        public const int MAX_DETAILS_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        public const int MAX_CARD_NUMBER_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        public string CardNumber { get; private set; } = default!;

        public string? OtherDetails { get; private set; } = default!;

        private DetailsForPayment(string cardNumber, string otherDetails)
        {
            CardNumber = cardNumber;
            OtherDetails = otherDetails;
        }

        public static DetailsForPayment Empty() => new DetailsForPayment(string.Empty, string.Empty);

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
