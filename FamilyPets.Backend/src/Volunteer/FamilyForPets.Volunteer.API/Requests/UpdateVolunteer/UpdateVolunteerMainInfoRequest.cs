using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerMainInfo;

namespace FamilyForPets.Volunteers.API.Requests.UpdateVolunteer
{
    public record UpdateVolunteerMainInfoRequest(
        string Name,
        string? Surname,
        string? AdditionalName,
        string? Description)
    {
        public UpdateVolunteerMainInfoCommand ToCommand(Guid id)
        {
            return new(id, new(Name, Surname, AdditionalName), Description);
        }
    }
}