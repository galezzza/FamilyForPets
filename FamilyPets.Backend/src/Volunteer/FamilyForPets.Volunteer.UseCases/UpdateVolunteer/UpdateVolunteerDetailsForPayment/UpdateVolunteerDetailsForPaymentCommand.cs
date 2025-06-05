using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public record UpdateVolunteerDetailsForPaymentCommand(
        Guid Id, PaymentDetailsDto Details) : ICommand;
}
