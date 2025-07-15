using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    public class FilePathsList : ValueObject
    {
        private List<FilePath> _filePaths = [];

        // for EF Core
        private FilePathsList()
        {
        }

        private FilePathsList(List<FilePath> filePaths)
        {
            _filePaths = filePaths;
        }

        public IReadOnlyCollection<FilePath> FilePaths => _filePaths.AsReadOnly();

        public static Result<FilePathsList, Error> Create(List<FilePath> filePaths)
        {
            return Result.Success<FilePathsList, Error>(
                new FilePathsList(filePaths));
        }

        public static FilePathsList Empty() => new FilePathsList([]);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _filePaths;
        }

    }
}