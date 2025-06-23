using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Volunteers.Domain.PetValueObjects
{
    public class PetPosition : ComparableValueObject
    {
        // ef core
        private PetPosition()
        {
        }

        private PetPosition(int positionNumber)
        {
            PositionNumber = positionNumber;
        }

        public int PositionNumber { get; }

        public static Result<PetPosition, Error> Create(int positionNumber)
        {
            if (positionNumber < 1)
                return Result.Failure<PetPosition, Error>(Errors.General.ValueIsInvalid(nameof(positionNumber)));
            return new PetPosition(positionNumber);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return PositionNumber;
        }
    }
}