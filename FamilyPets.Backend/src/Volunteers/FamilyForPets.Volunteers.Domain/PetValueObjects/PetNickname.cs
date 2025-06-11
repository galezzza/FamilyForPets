using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PetNickname : ComparableValueObject
    {
        public const int MAX_NAME_LENGHT = ProjectConstants.MAX_LOW_TEXT_LENGHT;

        private PetNickname(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static PetNickname Empty() => new PetNickname(string.Empty);

        public static Result<PetNickname, Error> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<PetNickname, Error>(Errors.General.CannotBeEmpty("Pet nickname"));
            return Result.Success<PetNickname, Error>(
                new PetNickname(name));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Name;
        }
    }
}