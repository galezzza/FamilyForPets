namespace FamilyForPets.UseCases.DTOs
{
    public record PaymentDetailsDto(
        string CardNumber,
        string? OtherPaymentDetails);
}
