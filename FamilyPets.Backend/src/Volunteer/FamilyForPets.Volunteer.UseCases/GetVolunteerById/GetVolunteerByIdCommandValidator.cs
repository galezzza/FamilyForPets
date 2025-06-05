using FamilyForPets.Shared;
using FamilyForPets.Shared.Validation;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.GetVolunteerById
{
    public class GetVolunteerByIdCommandValidator : AbstractValidator<GetVolunteerByIdCommand>
    {
        public GetVolunteerByIdCommandValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty).WithError(Errors.General.ValueIsInvalid("Volunteer ID"));
        }
    }
}