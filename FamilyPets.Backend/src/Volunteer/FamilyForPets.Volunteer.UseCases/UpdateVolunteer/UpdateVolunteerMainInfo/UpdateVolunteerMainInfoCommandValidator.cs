using FamilyForPets.Shared.Validation;
using FamilyForPets.Shared.ValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerMainInfo
{
    public class UpdateVolunteerMainInfoCommandValidator : AbstractValidator<UpdateVolunteerMainInfoCommand>
    {
        public UpdateVolunteerMainInfoCommandValidator()
        {
            RuleFor(c => c.FullName)
                .MustBeValueObject(x => FullName.Create(x.Name, x.Surname, x.AdditionalName));
        }
    }
}
