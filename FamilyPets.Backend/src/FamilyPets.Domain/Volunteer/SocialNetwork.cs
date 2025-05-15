using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Volunteer
{
    public class SocialNetwork : ValueObject
    {
        public string Name { get; private set; } = default!;

        public string Url { get; private set; } = default!;

        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public static Result<SocialNetwork> Create(string name, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<SocialNetwork>("Name cannot be empty");
            if (string.IsNullOrWhiteSpace(url))
                return Result.Failure<SocialNetwork>("Url cannot be empty");
            return Result.Success(new SocialNetwork(name, url));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Url;
        }
    }
}