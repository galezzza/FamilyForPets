using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.VolunteerValueObjects
{
    public class DetailsForPayment : ValueObject
    {
        public const int MAX_DETAILS_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        public const int MAX_CARD_NUMBER_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private DetailsForPayment(string cardNumber, string otherDetails)
        {
            CardNumber = cardNumber;
            OtherDetails = otherDetails;
        }

        public string CardNumber { get; private set; } = default!;

        public string OtherDetails { get; private set; } = string.Empty;

        public static DetailsForPayment Empty() => new DetailsForPayment(string.Empty, string.Empty);

        public static Result<DetailsForPayment, Error> Create(string cardNumber, string? otherDetails)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                return Result.Failure<DetailsForPayment, Error>(
                    Errors.General.CannotBeEmpty("Card number"));
            }

            if (string.IsNullOrEmpty(otherDetails))
            {
                return Result.Success<DetailsForPayment, Error>(
                    new DetailsForPayment(cardNumber, string.Empty));
            }

            return Result.Success<DetailsForPayment, Error>(
                new DetailsForPayment(cardNumber, otherDetails));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CardNumber;
        }
    }
}
