using System.Drawing;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyForPets.Infrastructure.Configurations
{
    public class ColorToInt32Converter : ValueConverter<Color, int>
    {
        public ColorToInt32Converter()
            : base(c => c.ToArgb(), v => Color.FromArgb(v)) { }
    }
}
