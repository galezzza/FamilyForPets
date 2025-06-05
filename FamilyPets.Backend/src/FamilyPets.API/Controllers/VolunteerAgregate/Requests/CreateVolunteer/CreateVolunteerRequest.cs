using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;

namespace FamilyForPets.API.Controllers.VolunteerAgregate.Requests.CreateVolunteer
{
    public record CreateVolunteerRequest(
        string Name,
        string? Surname,
        string? AdditionalName,
        string Email,
        int ExperienceInYears,
        string PhoneNumber,
        string CardNumber,
        string? OtherPaymentDetails)
    {
        public CreateVolunteerCommand ToCommand() =>
            new CreateVolunteerCommand(
                new Shared.DTOs.FullNameDto(
                    Name,
                    Surname,
                    AdditionalName),
                Email,
                ExperienceInYears,
                PhoneNumber,
                new Shared.DTOs.PaymentDetailsDto(
                    CardNumber,
                    OtherPaymentDetails));
    };
}
