using FamilyForPets.Shared.Validation;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment
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
