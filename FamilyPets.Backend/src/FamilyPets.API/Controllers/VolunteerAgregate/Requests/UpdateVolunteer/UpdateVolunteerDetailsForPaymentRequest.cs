using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment;

namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests.UpdateVolunteer
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