using FamilyForPets.Core.Validation;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerDetailsForPayment
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
