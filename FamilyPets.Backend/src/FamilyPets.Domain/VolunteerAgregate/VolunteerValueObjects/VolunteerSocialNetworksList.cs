using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects
{
    public class VolunteerSocialNetworksList : ValueObject
    {
        private List<SocialNetwork> _socialNetworks = [];

        public IReadOnlyCollection<SocialNetwork> SocialNetworks => _socialNetworks.AsReadOnly();

        private VolunteerSocialNetworksList(List<SocialNetwork> socialNetworks)
        {
            _socialNetworks = socialNetworks;
        }

        public static Result<VolunteerSocialNetworksList> Create(List<SocialNetwork> socialNetworks)
        {
            return Result.Success(new VolunteerSocialNetworksList(socialNetworks));
        }

        // check this!! Is it ok?
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _socialNetworks;
        }

        // for EF Core
        private VolunteerSocialNetworksList() { 
        }
    }
}