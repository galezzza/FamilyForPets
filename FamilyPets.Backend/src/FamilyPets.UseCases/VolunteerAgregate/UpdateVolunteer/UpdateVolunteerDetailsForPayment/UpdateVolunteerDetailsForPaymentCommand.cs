using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.DTOs;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public record UpdateVolunteerDetailsForPaymentCommand(
        Guid Id, PaymentDetailsDto Details) : ICommand;
}
