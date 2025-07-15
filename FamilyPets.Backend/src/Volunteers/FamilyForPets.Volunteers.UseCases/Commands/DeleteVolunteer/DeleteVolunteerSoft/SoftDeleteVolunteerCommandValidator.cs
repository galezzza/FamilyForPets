using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerSoft
{
    public class SoftDeleteVolunteerCommandValidator : AbstractValidator<SoftDeleteVolunteerCommand>
    {
        public SoftDeleteVolunteerCommandValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty)
                .WithError(Errors.General.ValueIsInvalid("Volunteer ID"));
        }
    }
}
