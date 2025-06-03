using FamilyForPets.Domain.SharedValueObjects;
using FamilyForPets.UseCases.Validation;
using FluentValidation;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerContactData
{
    public class UpdateVolunteerContactDataCommandValidator : AbstractValidator<UpdateVolunteerContactDataCommand>
    {
        public UpdateVolunteerContactDataCommandValidator()
        {
            RuleFor(cvc => cvc.Email)
                .MustBeValueObject(EmailAdress.Create);

            RuleFor(cvc => cvc.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);
        }
    }
}
