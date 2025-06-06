namespace FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer
{
    public record UpdateVolunteerDetailsForPaymentRequest(
        string CardNumber, string? OtherDetails);
}