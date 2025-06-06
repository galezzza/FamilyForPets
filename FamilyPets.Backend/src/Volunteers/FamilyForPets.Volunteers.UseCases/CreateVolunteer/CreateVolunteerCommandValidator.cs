using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.CreateVolunteer
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator()
        {
            RuleFor(cvc => cvc.FullName )
                .MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.AdditionalName));

            RuleFor(cvc => cvc.Email)
                .MustBeValueObject(EmailAdress.Create);

            RuleFor(cvc => cvc.ExperienceInYears).GreaterThan(-1).WithError(Errors.General.ValueIsInvalid("Volunteer Experience"));

            RuleFor(cvc => cvc.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);

            RuleFor(cvc => cvc.PaymentDetails)
                .MustBeValueObject(x => DetailsForPayment.Create(x.CardNumber, x.OtherPaymentDetails));
        }
    }
}
