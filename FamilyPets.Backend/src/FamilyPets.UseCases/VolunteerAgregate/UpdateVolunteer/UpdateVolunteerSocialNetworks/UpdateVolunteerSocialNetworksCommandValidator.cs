using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared.Validation;
using FluentValidation;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public class UpdateVolunteerSocialNetworksCommandValidator : AbstractValidator<UpdateVolunteerSocialNetworksCommand>
    {
        public UpdateVolunteerSocialNetworksCommandValidator()
        {
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