using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;

namespace FamilyForPets.Domain.SpeciesAgregate
{
    public class BreedId : CustomId<BreedId>, ICustomId<BreedId>
    {
        private BreedId(Guid id)
            : base(id) { }

        public static BreedId New() => new BreedId(Guid.NewGuid());

        public static BreedId Empty() => new BreedId(Guid.Empty);

        public static BreedId Create(Guid id) => new BreedId(id);

    }
}