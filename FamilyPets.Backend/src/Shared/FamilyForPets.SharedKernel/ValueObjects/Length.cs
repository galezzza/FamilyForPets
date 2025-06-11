using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class Length : ComparableValueObject
    {
        private Length() { }

        private Length(double value, LengthType type)
        {
            Value = value;
            Type = type;
        }

        public double Value { get; }

        public LengthType Type { get; }

        public static Result<Length, Error> Create(
            double value,
            LengthType? type)
        {
            if (type is null)
            {
                type = LengthType.Сentimeters;
            }

            return Result.Success<Length, Error>(new Length(value, type));
        }

        public static Length Empty() => new Length(0, LengthType.Сentimeters);

        public static Result<Length, Error> ConvertMassType(Length mass, LengthType toType)
        {
            if (mass.Type == toType)
            {
                return Result.Failure<Length, Error>(Errors.General.Conflict());
            }

            double valueMultiplier = GetValueMultiplier(mass.Type, toType);
            double newValue = mass.Value * valueMultiplier;

            return Result.Success<Length, Error>(new Length(newValue, toType));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }

        private static double GetValueMultiplier(LengthType currentType, LengthType toType)
        {
            // metre, centimetre, ...
            double[] tableOfMultipliersBaseOnKilograms = { 1, 0.01 };
            LengthType[] tableOfMassType = { LengthType.Metre, LengthType.Сentimeters };

            int currentTypeIndexInTable = 0;
            int toTypeIndexInTable = 0;

            for (int i = 0; i < tableOfMassType.Length; i += 1)
            {
                if (tableOfMassType[i] == currentType)
                {
                    currentTypeIndexInTable = i;
                }

                if (tableOfMassType[i] == toType)
                {
                    toTypeIndexInTable = i;
                }
            }

            double valueInKilograms = 1 /
                tableOfMultipliersBaseOnKilograms[currentTypeIndexInTable];

            double valueMultiplierToReturn = valueInKilograms *
                tableOfMultipliersBaseOnKilograms[toTypeIndexInTable];

            return valueMultiplierToReturn;
        }
    }
}