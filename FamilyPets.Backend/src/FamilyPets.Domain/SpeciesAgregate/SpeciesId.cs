using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;

namespace FamilyForPets.Domain.SpeciesAgregate
{

    public class SpeciesId : CustomId<SpeciesId>, ICustomId<SpeciesId>
    {
        private SpeciesId(Guid id)
            : base(id) { }

        public static SpeciesId New() => new SpeciesId(Guid.NewGuid());

        public static SpeciesId Empty() => new SpeciesId(Guid.Empty);

        public static SpeciesId Create(Guid id) => new SpeciesId(id);

    }
}