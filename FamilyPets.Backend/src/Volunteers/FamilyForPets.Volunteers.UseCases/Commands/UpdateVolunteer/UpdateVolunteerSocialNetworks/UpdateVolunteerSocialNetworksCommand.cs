using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public record UpdateVolunteerSocialNetworksCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks) : ICommand;
}