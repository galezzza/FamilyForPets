using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerContactData;

namespace FamilyForPets.Volunteers.API.Requests.UpdateVolunteer
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