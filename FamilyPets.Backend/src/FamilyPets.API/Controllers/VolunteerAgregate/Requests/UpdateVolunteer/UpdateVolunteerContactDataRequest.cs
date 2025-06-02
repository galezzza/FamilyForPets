using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerContactData;

namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests.UpdateVolunteer
{
    public record UpdateVolunteerContactDataRequest(
        string Email,
        string PhoneNumber)
    {
        public UpdateVolunteerContactDataCommand ToCommand(Guid id)
        {
            return new(id, Email, PhoneNumber);
        }
    }
}