using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class FilePath : ComparableValueObject
    {
        private FilePath() { }

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