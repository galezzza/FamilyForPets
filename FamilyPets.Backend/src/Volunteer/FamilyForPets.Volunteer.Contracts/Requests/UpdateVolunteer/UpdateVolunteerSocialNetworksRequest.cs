using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer
{
    public record UpdateVolunteerSocialNetworksRequest(
        IEnumerable<SocialNetworkDTO> SocialNetworks);
}