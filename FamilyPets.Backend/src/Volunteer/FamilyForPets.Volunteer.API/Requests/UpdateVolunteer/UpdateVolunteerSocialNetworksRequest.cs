using FamilyForPets.Shared.DTOs;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerSocialNetworks;

namespace FamilyForPets.Volunteers.API.Requests.UpdateVolunteer
{
    public record UpdateVolunteerSocialNetworksRequest(IEnumerable<SocialNetworkDTO> SocialNetworks)
    {
        public UpdateVolunteerSocialNetworksCommand ToCommand(Guid id)
        {
            return new UpdateVolunteerSocialNetworksCommand(id, SocialNetworks);
        }
    }
}