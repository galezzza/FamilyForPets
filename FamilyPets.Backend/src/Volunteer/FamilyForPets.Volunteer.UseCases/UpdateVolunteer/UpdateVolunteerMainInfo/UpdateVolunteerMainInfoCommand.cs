using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerMainInfo
{
    public record UpdateVolunteerMainInfoCommand(
        Guid Id,
        FullNameDto FullName,
        string? Description) : ICommand;
}
