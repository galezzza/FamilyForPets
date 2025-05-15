using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PetId : CustomId<PetId>
    {
        private PetId(Guid id)
            : base(id) { }

        public static PetId New() => new PetId(Guid.NewGuid());

    }
}
