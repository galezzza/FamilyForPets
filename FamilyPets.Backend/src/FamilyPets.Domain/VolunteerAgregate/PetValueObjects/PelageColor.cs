using System.Drawing;
using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PelageColor : ValueObject
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

        public static Result<PelageColor> Create(
            Color primaryColor,
            Color? secondaryColor,
            Color? tertiaryColor)
        {
            if (primaryColor.IsEmpty)
                return Result.Failure<PelageColor>("Primary color cannot be empty.");
            return Result.Success(new PelageColor(primaryColor, secondaryColor, tertiaryColor));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PrimaryColor;
            if (SecondaryColor.HasValue)
                yield return SecondaryColor.Value;
            if (TertiaryColor.HasValue)
                yield return TertiaryColor.Value;
        }
    }
}
