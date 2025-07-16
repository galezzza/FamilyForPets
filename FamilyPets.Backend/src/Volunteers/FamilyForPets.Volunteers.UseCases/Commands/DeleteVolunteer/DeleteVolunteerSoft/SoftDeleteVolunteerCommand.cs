using FamilyForPets.Core.Abstractions;

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerSoft
{
    public record SoftDeleteVolunteerCommand(Guid Id)
        : ICommand;
}
