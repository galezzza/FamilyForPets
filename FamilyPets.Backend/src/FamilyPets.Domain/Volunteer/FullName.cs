using CSharpFunctionalExtensions;

namespace FamilyForPets.Domain.Volunteer
{
    public class FullName : ValueObject
    {
        public string Name { get; } = default!;
        public string? Surname { get; }
        public string? AdditionalName { get; }

        private FullName(string name, string? surname, string? additionalName)
        {
            Name = name;
            Surname = surname;
            AdditionalName = additionalName;
        }

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