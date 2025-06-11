using System.Globalization;
using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class CastrationStatus : ComparableValueObject
    {
        public static readonly CastrationStatus Normal = new CastrationStatus(nameof(Normal));
        public static readonly CastrationStatus Neutered = new CastrationStatus(nameof(Neutered));

        private static readonly CastrationStatus[] _allStatuses =
        {
            Normal,
            Neutered,
        };

        private CastrationStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<CastrationStatus, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<CastrationStatus, Error>(Errors.General.ValueIsRequired());

            string status = input.Trim().ToLower(CultureInfo.InvariantCulture);

            if (_allStatuses.Any(g => g.Value.ToLowerInvariant() == status) == false)
                return Result.Failure<CastrationStatus, Error>(Errors.General.ValueIsInvalid("Castration Status"));

            return Result.Success<CastrationStatus, Error>(new CastrationStatus(status));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}