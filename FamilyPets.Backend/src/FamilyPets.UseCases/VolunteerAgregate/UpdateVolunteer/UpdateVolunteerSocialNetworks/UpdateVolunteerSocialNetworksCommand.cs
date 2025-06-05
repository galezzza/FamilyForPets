using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public record UpdateVolunteerSocialNetworksCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks) : ICommand;
}