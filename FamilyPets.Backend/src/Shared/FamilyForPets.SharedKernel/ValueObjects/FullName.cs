﻿using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class FullName : ComparableValueObject
    {
        public const int MAX_NAME_TEXT_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private FullName(string name, string surname, string additionalName)
        {
            Name = name;
            Surname = surname;
            AdditionalName = additionalName;
        }

        public string Name { get; } = default!;

        public string Surname { get; }

        public string AdditionalName { get; }

        public static Result<FullName, Error> Create(string name, string? surname, string? additionalName)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<FullName, Error>(Errors.General.CannotBeEmpty("Name"));
            if (string.IsNullOrWhiteSpace(surname))
                surname = string.Empty;
            if (string.IsNullOrWhiteSpace(additionalName))
                additionalName = string.Empty;
            return Result.Success<FullName, Error>(
                new FullName(name, surname, additionalName));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Name;
            yield return Surname;
            yield return AdditionalName;
        }
    }
}