namespace FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer
{
    public record UpdateVolunteerContactDataRequest(
        string Email,
        string PhoneNumber);
}