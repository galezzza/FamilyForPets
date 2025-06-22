using FamilyForPets.Files.Shared.DTOs;
using FamilyForPets.Files.UseCases;
using Minio;

namespace FamilyForPets.Files.Infrastructure.Providers
{
    public class MinioProvider : IFilesProvider
    {
        private readonly IMinioClient _minioClient;

        public MinioProvider(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public Task<string> GetPresignedUrlToUploadFullFileToFileService(FileName fileName) => throw new NotImplementedException();

        public Task<string> GetPresignedUrlToDownloadFullFileFromFileService(FileName fileName) => throw new NotImplementedException();

        public Task<string> MultipartUploadStart(FileName fileName, CancellationToken cancellationToken) => throw new NotImplementedException();

        public Task MultipartUploadCancel(FileName fileName, string uploadId, CancellationToken cancellationToken) => throw new NotImplementedException();

        public Task<string> MultipartUploadComplete(FileName fileName, string uploadId, List<PartETag> partETags, CancellationToken cancellationToken) => throw new NotImplementedException();

        public Task<string> GetPresignedUrlToUploadChunkOfFileToFileService(FileName fileName, string uploadId, int partNumber) => throw new NotImplementedException();

        public Task<string> DeleteFileFromFileService(FileName fileName, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
