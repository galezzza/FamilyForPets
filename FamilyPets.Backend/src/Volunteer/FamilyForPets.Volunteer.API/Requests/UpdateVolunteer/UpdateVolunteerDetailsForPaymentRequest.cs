using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment;

namespace FamilyForPets.Volunteers.API.Requests.UpdateVolunteer
{
    public record UpdateVolunteerDetailsForPaymentRequest(
        string CardNumber, string? OtherDetails)
    {
        public UpdateVolunteerDetailsForPaymentCommand ToCommand(Guid id)
        {
            return new UpdateVolunteerDetailsForPaymentCommand(id, new(CardNumber, OtherDetails));
        }
    }
}