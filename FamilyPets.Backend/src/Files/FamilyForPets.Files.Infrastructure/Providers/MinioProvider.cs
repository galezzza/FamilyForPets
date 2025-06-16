using FamilyForPets.Files.UseCases;

namespace FamilyForPets.Files.Infrastructure.Providers
{
    public class MinioProvider : IFilesProvider
    {
        public Guid Test()
        {
            return Guid.NewGuid();
        }
    }
}
