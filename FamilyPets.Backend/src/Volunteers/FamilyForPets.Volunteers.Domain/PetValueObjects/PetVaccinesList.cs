﻿using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
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

        public static Result<PetVaccinesList, Error> Create(List<PetVaccine> petVaccines)
        {
            return Result.Success<PetVaccinesList, Error>(
                new PetVaccinesList(petVaccines));
        }

        public static PetVaccinesList Empty() => new PetVaccinesList([]);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _petVaccines;
        }

    }
}