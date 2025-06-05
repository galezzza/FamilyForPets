using CSharpFunctionalExtensions;
using FamilyForPets.Shared;

namespace FamilyForPets.Domain.VolunteerAgregate.PetValueObjects
{
    public class PetVaccinesList : ValueObject
    {
        private List<PetVaccine> _petVaccines = [];

        // for EF Core
        private PetVaccinesList()
        {
        }

        private PetVaccinesList(List<PetVaccine> petVaccine)
        {
            _petVaccines = petVaccine;
        }

        public IReadOnlyCollection<PetVaccine> PetVaccines => _petVaccines.AsReadOnly();

        public static Result<PetVaccinesList, Error> Create(List<PetVaccine> socialNetworks)
        {
            return Result.Success<PetVaccinesList, Error>(
                new PetVaccinesList(socialNetworks));
        }

        public static PetVaccinesList Empty() => new PetVaccinesList([]);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _petVaccines;
        }

    }
}