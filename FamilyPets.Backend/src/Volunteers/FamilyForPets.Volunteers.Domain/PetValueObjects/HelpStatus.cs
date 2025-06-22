using System.Globalization;
using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class HelpStatus : ComparableValueObject
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

        private HelpStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<HelpStatus, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<HelpStatus, Error>(Errors.General.ValueIsRequired());

            var status = input.Trim().ToLower(CultureInfo.InvariantCulture);

            if (_allStatuses.Any(g => g.Value.ToLowerInvariant() == status) == false)
                return Result.Failure<HelpStatus, Error>(Errors.General.ValueIsInvalid("Status"));

            return Result.Success<HelpStatus, Error>(new HelpStatus(status));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
