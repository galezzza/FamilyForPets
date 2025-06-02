using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.UseCases.Validation;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks;
using FluentValidation;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public class UpdateVolunteerDetailsForPaymentCommandValidator : AbstractValidator<UpdateVolunteerDetailsForPaymentCommand>
    {
        public UpdateVolunteerDetailsForPaymentCommandValidator()
        {
            RuleFor(c => c.details)
                .MustBeValueObject(x => DetailsForPayment.Create(x.CardNumber, x.OtherPaymentDetails));
        }
    }
}
