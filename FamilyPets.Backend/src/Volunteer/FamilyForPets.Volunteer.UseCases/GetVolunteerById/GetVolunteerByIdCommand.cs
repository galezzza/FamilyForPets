using FamilyForPets.Shared.Abstractions;

namespace FamilyForPets.Volunteers.UseCases.GetVolunteerById
{
    public record GetVolunteerByIdCommand(
        Guid Id) : ICommand;
}