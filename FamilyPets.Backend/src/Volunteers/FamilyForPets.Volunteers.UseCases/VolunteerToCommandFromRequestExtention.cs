using FamilyForPets.Core.DTOs;
using FamilyForPets.Volunteers.Contracts.Requests.CreateVolunteer;
using FamilyForPets.Volunteers.UseCases.Commands.CreateVolunteer;

namespace FamilyForPets.Volunteers.UseCases
{
    public static class VolunteerToCommandFromRequestExtention
    {
        public static CreateVolunteerCommand ToCommand(this CreateVolunteerRequest request)
        {
            return new CreateVolunteerCommand(
                new FullNameDto(request.Name, request.Surname, request.AdditionalName),
                request.Email,
                request.ExperienceInYears,
                request.PhoneNumber,
                new PaymentDetailsDto(request.CardNumber, request.OtherPaymentDetails));
        }
    }
}
