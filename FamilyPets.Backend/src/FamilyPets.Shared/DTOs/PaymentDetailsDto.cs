namespace FamilyForPets.Shared.DTOs
{
    public record PaymentDetailsDto(
        string CardNumber,
        string? OtherPaymentDetails);
}
