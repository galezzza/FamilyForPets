using FamilyForPets.UseCases.DTOs;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks;

namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests.UpdateVolunteer
{
    public record UpdateVolunteerRequest(
        IEnumerable<SocialNetworkDTO> SocialNetworks,
        string CardNumber, string? OtherDetails,
        string Name, string? Surname, string? AdditionalName, string? Description,
        string Email, string PhoneNumber)
    {
        public UpdateVolunteerCommand ToCommand(Guid id)
        {
            return new UpdateVolunteerCommand(
                id,
                SocialNetworks,
                new PaymentDetailsDto(CardNumber, OtherDetails),
                new FullNameDto(
                    Name, Surname, AdditionalName),
                Description,
                PhoneNumber, Email);
        }
    }
}