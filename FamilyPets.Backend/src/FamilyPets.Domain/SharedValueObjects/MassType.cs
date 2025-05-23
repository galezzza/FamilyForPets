using System.Globalization;
using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.SharedValueObjects
{
    public class MassType : ValueObject
    {
        public static readonly MassType Kilograms = new MassType(nameof(Kilograms));
        public static readonly MassType Pounds = new MassType(nameof(Pounds));

        private static readonly MassType[] _allTypes =
        {
            Kilograms,
            Pounds,
        };

        private MassType(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<MassType, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<MassType, Error>(Errors.General.ValueIsRequired());

            var status = input.Trim().ToLower(CultureInfo.InvariantCulture);

            if (_allTypes.Any(g => g.Value.ToLowerInvariant() == status) == false)
                return Result.Failure<MassType, Error>(Errors.General.ValueIsInvalid("Mass type"));

            return Result.Success<MassType, Error>(new MassType(status));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}