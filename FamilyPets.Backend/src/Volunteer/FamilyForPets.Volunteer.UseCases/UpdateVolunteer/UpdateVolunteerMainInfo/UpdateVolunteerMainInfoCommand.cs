using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerMainInfo
{
    public record UpdateVolunteerMainInfoCommand(
        Guid Id,
        FullNameDto FullName,
        string? Description) : ICommand;
}
