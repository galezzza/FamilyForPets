using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerMainInfo;

namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests.UpdateVolunteer
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