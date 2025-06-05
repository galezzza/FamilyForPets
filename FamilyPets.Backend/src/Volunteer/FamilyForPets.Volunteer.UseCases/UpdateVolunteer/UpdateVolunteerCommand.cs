using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer
{
    public record UpdateVolunteerCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks,
        PaymentDetailsDto PaymentDetails,
        FullNameDto FullName, string? Description,
        string PhoneNumber, string EmailAdress) : ICommand;
}
