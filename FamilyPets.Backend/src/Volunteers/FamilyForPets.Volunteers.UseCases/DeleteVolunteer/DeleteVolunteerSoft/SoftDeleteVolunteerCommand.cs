using FamilyForPets.Core.Abstractions;
using FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerHard;

namespace FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerSoft
{
    public record SoftDeleteVolunteerCommand(Guid Id)
        : ICommand;
}
