using FamilyForPets.Shared.DTOs;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer;

namespace FamilyForPets.Volunteers.API.Requests.UpdateVolunteer
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