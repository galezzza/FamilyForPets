using FamilyForPets.UseCases.Abstractions;

namespace FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer
{
    public record CreateVolunteerCommand(
        FullNameDto FullName,
        string Email,
        int ExperienceInYears,
        string PhoneNumber,
        PaymentDetailsDto PaymentDetails) : ICommand;

    public record FullNameDto(
        string Name,
        string? Surname,
        string? AdditionalName);

    public record PaymentDetailsDto(
        string CardNumber,
        string? OtherPaymentDetails);
}
