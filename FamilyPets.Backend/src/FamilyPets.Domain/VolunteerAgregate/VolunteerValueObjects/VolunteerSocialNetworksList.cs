using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects
{
    public class VolunteerSocialNetworksList : ValueObject
    {
        private List<SocialNetwork> _socialNetworks = [];

        // for EF Core
        private VolunteerSocialNetworksList()
        {
        }

        private VolunteerSocialNetworksList(List<SocialNetwork> socialNetworks)
        {
            _socialNetworks = socialNetworks;
        }

        public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks.AsReadOnly();

        public static Result<VolunteerSocialNetworksList, Error> Create(List<SocialNetwork> socialNetworks)
        {
            return Result.Success<VolunteerSocialNetworksList, Error>(
                new VolunteerSocialNetworksList(socialNetworks));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _socialNetworks;
        }

    }
}