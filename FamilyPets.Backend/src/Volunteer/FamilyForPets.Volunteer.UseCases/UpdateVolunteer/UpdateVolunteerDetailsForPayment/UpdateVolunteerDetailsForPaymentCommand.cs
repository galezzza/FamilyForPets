using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public record UpdateVolunteerDetailsForPaymentCommand(
        Guid Id, PaymentDetailsDto Details) : ICommand;
}
