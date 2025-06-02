using FamilyForPets.UseCases.Abstractions;

namespace FamilyForPets.UseCases.VolunteerAgregate.GetVolunteerById
{
    public record GetVolunteerByIdCommand(
        Guid Id) : ICommand;
}