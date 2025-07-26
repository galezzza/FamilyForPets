using FamilyForPets.Core.Abstractions;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerContactData
{
    public record UpdateVolunteerContactDataCommand(
        Guid Id,
        string Email,
        string PhoneNumber) : ICommand;
}
