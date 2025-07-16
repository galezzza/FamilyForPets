using CSharpFunctionalExtensions;

namespace FamilyForPets.SharedKernel.ValueObjects
{
    //public record FileName(string Key, string BucketName);

    public class FileName : ComparableValueObject
    {

        private FileName(string key, string bucketName)
        {
            Key = key;
            BucketName = bucketName;
        }

        public string Key { get; }

        public string BucketName { get; }

        public static Result<FileName, Error> Create(string key, string bucketName)
        {
            return Result.Success<FileName, Error>(new FileName(key, bucketName));
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Key;
            yield return BucketName;
        }
    }
}
