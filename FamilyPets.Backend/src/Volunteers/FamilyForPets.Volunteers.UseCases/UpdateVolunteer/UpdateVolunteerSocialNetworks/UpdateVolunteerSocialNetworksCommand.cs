using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public record UpdateVolunteerSocialNetworksCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks) : ICommand;
}