using System.Globalization;
using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.SharedValueObjects
{
    public class LengthType : ValueObject
    {
        public static readonly LengthType Metre = new LengthType(nameof(Metre));
        public static readonly LengthType Сentimeters = new LengthType(nameof(Сentimeters));

        private static readonly LengthType[] _allTypes =
        {
            Metre,
            Сentimeters,
        };

        private LengthType(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<LengthType, Error> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<LengthType, Error>(Errors.General.ValueIsRequired());

            string status = input.Trim().ToLower(CultureInfo.InvariantCulture);

            if (_allTypes.Any(g => g.Value.ToLowerInvariant() == status) == false)
                return Result.Failure<LengthType, Error>(Errors.General.ValueIsInvalid("Length type"));

            return Result.Success<LengthType, Error>(new LengthType(status));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}