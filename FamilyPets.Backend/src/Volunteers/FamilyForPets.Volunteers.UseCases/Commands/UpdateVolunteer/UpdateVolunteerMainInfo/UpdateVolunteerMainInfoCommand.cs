using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerMainInfo
{
    public record UpdateVolunteerMainInfoCommand(
        Guid Id,
        FullNameDto FullName,
        string? Description) : ICommand;
}
