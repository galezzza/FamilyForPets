using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public record UpdateVolunteerSocialNetworksCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks) : ICommand;
}