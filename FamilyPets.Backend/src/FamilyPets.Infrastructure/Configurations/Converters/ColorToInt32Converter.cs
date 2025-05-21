using System.Drawing;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyForPets.Infrastructure.Configurations.Converters
{
    public class ColorToInt32Converter : ValueConverter<Color, int>
    {
        public ColorToInt32Converter()
            : base(c => c.ToArgb(), v => Color.FromArgb(v)) { }
    }
}
