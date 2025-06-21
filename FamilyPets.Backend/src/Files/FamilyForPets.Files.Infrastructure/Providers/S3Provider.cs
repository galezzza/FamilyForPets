using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using FamilyForPets.Files.UseCases;

namespace FamilyForPets.Files.Infrastructure.Providers
{
    public class S3Provider : IFilesProvider
    {
        private readonly IAmazonS3 _s3Client;

        public S3Provider(IAmazonS3 amazonClient)
        {
            _s3Client = amazonClient;
        }

        public Task GetPresignedUrlToDownloadFullFileFromFileService() => throw new NotImplementedException();

        public Task GetPresignedUrlToDownloadSingleFile() => throw new NotImplementedException();

        public Task GetPresignedUrlToUploadChunkOfFileToFileService() => throw new NotImplementedException();

        public Task GetPresignedUrlToUploadFullFileToFileService() => throw new NotImplementedException();

        public Task GetPresignedUrlToUploadSingleFile() => throw new NotImplementedException();

        public Task MultipartUploadCancel() => throw new NotImplementedException();

        public Task MultipartUploadComplete() => throw new NotImplementedException();

        public Task MultipartUploadStart() => throw new NotImplementedException();

        public Task DeleteFileFromFileService() => throw new NotImplementedException();
    }
}
