using System.Drawing;
using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PelageColor : ComparableValueObject
    {
        private PelageColor(Color primaryColor, Color? secondaryColor, Color? tertiaryColor)
        {
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            TertiaryColor = tertiaryColor;
        }

        public Color PrimaryColor { get; }

        public Color? SecondaryColor { get; }

        public Color? TertiaryColor { get; }

        public static Result<PelageColor, Error> Create(
            Color primaryColor,
            Color? secondaryColor,
            Color? tertiaryColor)
        {
            if (primaryColor.IsEmpty)
                return Result.Failure<PelageColor, Error>(Errors.General.CannotBeEmpty("Primary color"));
            return Result.Success<PelageColor, Error>(
                new PelageColor(primaryColor, secondaryColor, tertiaryColor));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return PrimaryColor;
            if (SecondaryColor.HasValue)
                yield return SecondaryColor.Value;
            if (TertiaryColor.HasValue)
                yield return TertiaryColor.Value;
        }
    }
}
