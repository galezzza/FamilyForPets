using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerHard
{
    public class HardDeleteVolunteerCommandValidator : AbstractValidator<HardDeleteVolunteerCommand>
    {
        public HardDeleteVolunteerCommandValidator()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty)
                .WithError(Errors.General.ValueIsInvalid("Volunteer ID"));
        }
    }
}
