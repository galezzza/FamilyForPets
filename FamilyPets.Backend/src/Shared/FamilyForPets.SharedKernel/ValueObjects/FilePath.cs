using CSharpFunctionalExtensions;
using FamilyForPets.Core.DTOs;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Domain.PetValueObjects;

namespace FamilyForPets.Volunteers.Domain.Entities
{
    public class FilePath : ComparableValueObject
    {

        private FilePath(FileName filePath) {
            Path = filePath;
        }

        public FileName Path { get; }

        public static Result<FilePath, Error> Create(FileName path)
        {
            return Result.Success<FilePath, Error>(new FilePath(path));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Path;
        }
    }
}