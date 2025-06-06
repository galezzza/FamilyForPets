using FamilyForPets.SharedKernel;
using FamilyForPets.Shared.Validation;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer
{
    public class UpdateVolunteerCommandValidator : AbstractValidator<UpdateVolunteerCommand>
    {
        public UpdateVolunteerCommandValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty)
                .WithError(Errors.General.ValueIsInvalid("Volunteer ID"));

            RuleFor(cv => cv.EmailAdress)
                .MustBeValueObject(EmailAdress.Create);

            RuleFor(cv => cv.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);

            RuleFor(cv => cv.PaymentDetails)
                .MustBeValueObject(x => DetailsForPayment.Create(x.CardNumber, x.OtherPaymentDetails));

            RuleFor(c => c.FullName)
                .MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.AdditionalName));

            RuleFor(c => c.SocialNetworks)
                .ForEach(validator =>
                validator.ChildRules(socialNetwork =>
                {
                    socialNetwork.RuleFor(sn => new { sn.Name, sn.Url })
                    .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.Url));
                }));
        }
    }
}
