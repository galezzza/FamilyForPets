using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer
{
    public record UpdateVolunteerCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks,
        PaymentDetailsDto PaymentDetails,
        FullNameDto FullName, string? Description,
        string PhoneNumber, string EmailAdress) : ICommand;
}
