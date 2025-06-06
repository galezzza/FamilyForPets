using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class FullName : ValueObject
    {
        public const int MAX_NAME_TEXT_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private FullName(string name, string? surname, string? additionalName)
        {
            Name = name;
            Surname = surname;
            AdditionalName = additionalName;
        }

        public string Name { get; } = default!;

        public string? Surname { get; }

        public string? AdditionalName { get; }

        public static Result<FullName, Error> Create(string name, string? surname, string? additionalName)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<FullName, Error>(Errors.General.CannotBeEmpty("Name"));
            return Result.Success<FullName, Error>(
                new FullName(name, surname, additionalName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}