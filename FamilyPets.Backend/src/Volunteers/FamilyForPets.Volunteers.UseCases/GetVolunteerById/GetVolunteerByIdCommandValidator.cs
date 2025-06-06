using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
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