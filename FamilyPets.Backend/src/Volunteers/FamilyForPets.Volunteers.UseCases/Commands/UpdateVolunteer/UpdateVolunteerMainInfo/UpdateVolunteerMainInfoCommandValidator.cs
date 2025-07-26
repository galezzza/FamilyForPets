using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel.ValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerMainInfo
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
