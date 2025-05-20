using CSharpFunctionalExtensions;
using FamilyForPets.Domain.Shared;

namespace FamilyForPets.Domain
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

        public static Result<FullName> Create(string name, string? surname, string? additionalName)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<FullName>("Name cannot be empty");
            return Result.Success(new FullName(name, surname, additionalName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}