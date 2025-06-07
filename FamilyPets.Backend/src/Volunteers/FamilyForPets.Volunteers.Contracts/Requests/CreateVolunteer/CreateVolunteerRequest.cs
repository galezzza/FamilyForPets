namespace FamilyForPets.Volunteers.Contracts.Requests.CreateVolunteer
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
