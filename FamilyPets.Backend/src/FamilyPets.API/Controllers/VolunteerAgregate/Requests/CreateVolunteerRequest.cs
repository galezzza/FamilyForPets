namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests
{
    public record CreateVolunteerRequest(
        string Name,
        string? Surname,
        string? AdditionalName,
        string Email,
        int ExperienceInYears,
        string PhoneNumber,
        string CardNumber,
        string? OtherPaymentDetails);
}
