using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.VolunteerValueObjects
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

        public static Result<VolunteerSocialNetworksList, Error> Create(IEnumerable<SocialNetwork> socialNetworks)
        {
            return Result.Success<VolunteerSocialNetworksList, Error>(
                new VolunteerSocialNetworksList(socialNetworks.ToList()));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _socialNetworks;
        }

    }
}