namespace FamilyForPets.Core.DTOs
{
    public record PaymentDetailsDto(
        string CardNumber,
        string? OtherPaymentDetails);
}
