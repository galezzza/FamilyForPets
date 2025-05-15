using FamilyForPets.Domain.VolunteerAgregate;

namespace FamilyForPets.Domain.Species
{

    public class SpeciesId : CustomId<SpeciesId>
    {
        private SpeciesId(Guid id)
            : base(id) { }

        public static SpeciesId NewVolunteerId() => new SpeciesId(Guid.NewGuid());

    }
}