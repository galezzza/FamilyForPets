using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate.PetValueObjects;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;

namespace FamilyForPets.Domain.VolunteerAgregate
{

    public class VolunteerId : CustomId<VolunteerId>
    {
        private VolunteerId(Guid id)
            : base(id) { }

        public static VolunteerId NewVolunteerId() => new VolunteerId(Guid.NewGuid());

    }

}
