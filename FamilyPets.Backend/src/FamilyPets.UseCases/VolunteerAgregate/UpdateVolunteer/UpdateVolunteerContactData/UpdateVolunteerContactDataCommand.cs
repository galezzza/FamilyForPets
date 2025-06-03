using FamilyForPets.UseCases.Abstractions;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerContactData
{
    public record UpdateVolunteerContactDataCommand(
        Guid Id,
        string Email,
        string PhoneNumber) : ICommand;
}
