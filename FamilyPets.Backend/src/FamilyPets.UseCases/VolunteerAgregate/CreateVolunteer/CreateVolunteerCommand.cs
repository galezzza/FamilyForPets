using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer
{
    public record CreateVolunteerCommand(
        FullNameDto FullName,
        string Email,
        int ExperienceInYears,
        string PhoneNumber,
        PaymentDetailsDto PaymentDetails) : ICommand;
}
