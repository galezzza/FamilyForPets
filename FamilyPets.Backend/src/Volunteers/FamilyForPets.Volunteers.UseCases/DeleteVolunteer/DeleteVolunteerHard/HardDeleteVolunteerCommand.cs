using FamilyForPets.Core.Abstractions;
using FamilyForPets.Volunteers.UseCases.GetVolunteerById;

namespace FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerHard
{
    public record HardDeleteVolunteerCommand(Guid Id)
        : ICommand;
}
