using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared.Validation;
using FluentValidation;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public class UpdateVolunteerDetailsForPaymentCommandValidator : AbstractValidator<UpdateVolunteerDetailsForPaymentCommand>
    {
        public UpdateVolunteerDetailsForPaymentCommandValidator()
        {
            RuleFor(c => c.Details)
                .MustBeValueObject(x => DetailsForPayment.Create(x.CardNumber, x.OtherPaymentDetails));
        }
    }
}
