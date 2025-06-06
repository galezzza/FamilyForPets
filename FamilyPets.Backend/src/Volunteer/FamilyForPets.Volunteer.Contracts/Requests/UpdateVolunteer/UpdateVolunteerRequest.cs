using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer
{
    public record UpdateVolunteerRequest(
        IEnumerable<SocialNetworkDTO> SocialNetworks,
        string CardNumber, string? OtherDetails,
        string Name, string? Surname, string? AdditionalName, string? Description,
        string Email, string PhoneNumber);
}
