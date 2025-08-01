﻿using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PetVaccine : ComparableValueObject
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private PetVaccine(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;


        public static Result<PetVaccine, Error> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<PetVaccine, Error>(Errors.General.CannotBeEmpty("Name"));
            return Result.Success<PetVaccine, Error>(new PetVaccine(name));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Name;
        }
    }
}