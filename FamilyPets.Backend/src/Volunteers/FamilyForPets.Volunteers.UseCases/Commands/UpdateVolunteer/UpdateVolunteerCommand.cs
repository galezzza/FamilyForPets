using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer
{
    public record UpdateVolunteerCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks,
        PaymentDetailsDto PaymentDetails,
        FullNameDto FullName, string? Description,
        string PhoneNumber, string EmailAdress) : ICommand;
}
