using FamilyForPets.Core.Abstractions;

namespace FamilyForPets.Volunteers.UseCases.Commands.GetVolunteerById
{
    public record GetVolunteerByIdCommand(
        Guid Id) : ICommand;
}