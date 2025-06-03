using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerMainInfo
{
    public record UpdateVolunteerMainInfoCommand(
        Guid Id,
        FullNameDto FullName,
        string? Description) : ICommand;
}
