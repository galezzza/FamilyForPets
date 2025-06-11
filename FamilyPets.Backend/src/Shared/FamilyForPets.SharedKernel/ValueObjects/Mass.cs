using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class Mass : ComparableValueObject
    {
        private Mass() { }

        private Mass(double value, MassType type)
        {
            Value = value;
            Type = type;
        }

        public double Value { get; }

        public MassType Type { get; }

        public static Result<Mass, Error> Create(
            double value,
            MassType? type)
        {
            if (type is null)
            {
                type = MassType.Kilograms;
            }

            return Result.Success<Mass, Error>(new Mass(value, type));
        }

        public static Mass Empty() => new Mass(0, MassType.Kilograms);

        public static Result<Mass, Error> ConvertMassType(Mass mass, MassType toType)
        {
            if (mass.Type == toType)
            {
                return Result.Failure<Mass, Error>(Errors.General.Conflict());
            }

            double valueMultiplier = GetValueMultiplier(mass.Type, toType);
            double newValue = mass.Value * valueMultiplier;

            return Result.Success<Mass, Error>(new Mass(newValue, toType));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }

        private static double GetValueMultiplier(MassType currentType, MassType toType)
        {
            // kilograms, pounds, ...
            double[] tableOfMultipliersBaseOnKilograms = { 1, 2.2 };
            MassType[] tableOfMassType = { MassType.Kilograms, MassType.Pounds};

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
