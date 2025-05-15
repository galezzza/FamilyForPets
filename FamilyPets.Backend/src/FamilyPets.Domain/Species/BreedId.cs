using FamilyForPets.Domain.VolunteerAgregate;

namespace FamilyForPets.Domain.Species
{
    public class BreedId : CustomId<BreedId>
    {
        private BreedId(Guid id)
            : base(id) { }

        public static BreedId NewVolunteerId() => new BreedId(Guid.NewGuid());

    }
}