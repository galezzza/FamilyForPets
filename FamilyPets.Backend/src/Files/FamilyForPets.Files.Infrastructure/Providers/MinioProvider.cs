using FamilyForPets.Files.Domain.DTOs;
using FamilyForPets.Files.UseCases;
using FamilyForPets.SharedKernel.ValueObjects;
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

        public Task<string> GetPresignedUrlToUploadFullFile(FileName fileName)
            => throw new NotImplementedException();

        public Task<string> GetPresignedUrlToDownloadFullFile(FileName fileName)
            => throw new NotImplementedException();

        public Task<string> MultipartUploadStart(
            FileName fileName, string contentType, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task MultipartUploadCancel(
            FileName fileName, string uploadId, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task<string> MultipartUploadComplete(
            FileName fileName, string uploadId, List<PartETag> partETags, CancellationToken cancellationToken) 
            => throw new NotImplementedException();

        public Task<string> GetPresignedUrlToUploadChunkOfFile(
            FileName fileName, string uploadId, int partNumber)
            => throw new NotImplementedException();

        public Task<string> DeleteFileFromFileService(
            FileName fileName, CancellationToken cancellationToken)
            => throw new NotImplementedException();
    }
}
