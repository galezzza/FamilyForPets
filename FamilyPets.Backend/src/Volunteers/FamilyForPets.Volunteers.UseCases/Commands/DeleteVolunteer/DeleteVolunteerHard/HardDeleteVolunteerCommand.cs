using FamilyForPets.Core.Abstractions;
using FamilyForPets.Volunteers.UseCases.GetVolunteerById;

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerHard
{
    public record HardDeleteVolunteerCommand(Guid Id)
        : ICommand;
}
