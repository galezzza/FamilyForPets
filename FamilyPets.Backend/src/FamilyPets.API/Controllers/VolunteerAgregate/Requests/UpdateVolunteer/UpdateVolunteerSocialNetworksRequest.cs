using FamilyForPets.UseCases.DTOs;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks;

namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests.UpdateVolunteer
{
    public record UpdateVolunteerSocialNetworksRequest(IEnumerable<SocialNetworkDTO> SocialNetworks)
    {
        public UpdateVolunteerSocialNetworksCommand ToCommand(Guid id)
        {
            return new UpdateVolunteerSocialNetworksCommand(id, SocialNetworks);
        }
    }
}