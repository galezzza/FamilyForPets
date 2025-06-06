using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.CreateVolunteer
{
    public record CreateVolunteerCommand(
        FullNameDto FullName,
        string Email,
        int ExperienceInYears,
        string PhoneNumber,
        PaymentDetailsDto PaymentDetails) : ICommand;
}
