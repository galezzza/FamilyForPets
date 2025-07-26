using FamilyForPets.Core.Abstractions;

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerHard
{
    public record HardDeleteVolunteerCommand(Guid Id)
        : ICommand;
}
