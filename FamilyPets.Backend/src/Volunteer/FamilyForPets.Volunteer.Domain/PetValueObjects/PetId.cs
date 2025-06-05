using FamilyForPets.Shared.ValueObjects.IDs;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PetId : CustomId<PetId>, ICustomId<PetId>
    {
        private PetId(Guid id)
            : base(id) { }

        public static PetId New() => new PetId(Guid.NewGuid());

        public static PetId Empty() => new PetId(Guid.Empty);

        public static PetId Create(Guid id) => new PetId(id);

    }
}
