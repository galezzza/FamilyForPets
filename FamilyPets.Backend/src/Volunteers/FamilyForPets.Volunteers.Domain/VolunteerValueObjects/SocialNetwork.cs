using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.VolunteerValueObjects
{
    public class SocialNetwork : ValueObject
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        public const int MAX_URL_LENGHT = ProjectConstants.MAX_HIGH_TEXT_LENGHT;

        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; private set; } = default!;

        public string Url { get; private set; } = default!;

        public static Result<SocialNetwork, Error> Create(string name, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<SocialNetwork, Error>(Errors.General.CannotBeEmpty("Name"));
            if (string.IsNullOrWhiteSpace(url))
                return Result.Failure<SocialNetwork, Error>(Errors.General.CannotBeEmpty("Url"));
            return Result.Success<SocialNetwork, Error>(new SocialNetwork(name, url));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Url;
        }
    }
}