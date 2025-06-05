using FamilyForPets.Shared.ValueObjects.IDs;

namespace FamilyForPets.Volunteers.Domain.VolunteerValueObjects
{

    public class VolunteerId : CustomId<VolunteerId>, ICustomId<VolunteerId>
    {
        private VolunteerId(Guid id)
            : base(id) { }

        public static VolunteerId New() => new VolunteerId(Guid.NewGuid());

        public static VolunteerId Empty() => new VolunteerId(Guid.Empty);

        public static VolunteerId Create(Guid id) => new VolunteerId(id);

    }

}
