using System.Globalization;
using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class HelpStatus : ValueObject
    {
        public static readonly HelpStatus HelpNeeded = new HelpStatus(nameof(HelpNeeded));
        public static readonly HelpStatus LookingForHome = new HelpStatus(nameof(LookingForHome));
        public static readonly HelpStatus HomeFounded = new HelpStatus(nameof(HomeFounded));

        private static readonly HelpStatus[] _allStatuses =
        {
            HelpNeeded,
            LookingForHome,
            HomeFounded,
        };

        public string Value { get; }

        private HelpStatus(string value)
        {
            Value = value;
        }

        public static Result<HelpStatus> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<HelpStatus>("Value cannot be empty.");

            var status = input.Trim().ToLower(CultureInfo.InvariantCulture);

            if (_allStatuses.Any(g => g.Value.ToLowerInvariant() == status) == false)
                return Result.Failure<HelpStatus>($"Invalid status: {input}");

            return Result.Success(new HelpStatus(status));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
