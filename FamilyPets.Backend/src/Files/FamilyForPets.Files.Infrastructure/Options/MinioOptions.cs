namespace FamilyForPets.Files.Infrastructure.Options
{
    public class MinioOptions
    {
        public static readonly string MINIO = "Minio";

        public string Endpoint { get; init; } = string.Empty;

        public string AccessKey { get; init; } = string.Empty;

        public string SecretKey { get; init; } = string.Empty;

        public bool IsWithSSL { get; init; } = false;
    }
}
