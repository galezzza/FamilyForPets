using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public record UpdateVolunteerDetailsForPaymentCommand(
        Guid Id, PaymentDetailsDto Details) : ICommand;
}
