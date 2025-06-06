namespace FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer
{
    public record UpdateVolunteerMainInfoRequest(
        string Name,
        string? Surname,
        string? AdditionalName,
        string? Description);
}