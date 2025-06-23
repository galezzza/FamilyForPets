using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using FamilyForPets.Core.DTOs;
using FamilyForPets.Files.Infrastructure.Options;
using FamilyForPets.Files.UseCases;
using FamilyForPets.SharedKernel.ValueObjects;
using Minio.DataModel;

namespace FamilyForPets.Files.Infrastructure.Providers
{
    public class S3Provider : IFilesProvider
    {
        private readonly IAmazonS3 _s3Client;
        private readonly MinioOptions _minioOptions;

        public S3Provider(IAmazonS3 amazonClient, MinioOptions minioOptions)
        {
            _s3Client = amazonClient;
            _minioOptions = minioOptions;
        }

        public async Task<string> GetPresignedUrlToUploadFullFile(
            FileName fileName)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(60),
                Protocol = _minioOptions.IsWithSSL ? Protocol.HTTPS : Protocol.HTTP,
            };

            return await _s3Client.GetPreSignedURLAsync(request);
        }

        public async Task<string> GetPresignedUrlToDownloadFullFile(
            FileName fileName)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(24),
                Protocol = _minioOptions.IsWithSSL ? Protocol.HTTPS : Protocol.HTTP,
            };

            return await _s3Client.GetPreSignedURLAsync(request);
        }

        public async Task<string> MultipartUploadStart(
            FileName fileName,
            string contentType,
            CancellationToken cancellationToken)
        {
            var initiateRequest = new InitiateMultipartUploadRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
                ContentType = contentType,
            };

            InitiateMultipartUploadResponse result = await _s3Client
                .InitiateMultipartUploadAsync(initiateRequest, cancellationToken);

            return result.UploadId;
        }

        public async Task MultipartUploadCancel(
            FileName fileName,
            string uploadId,
            CancellationToken cancellationToken)
        {
            var abortRequest = new AbortMultipartUploadRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
                UploadId = uploadId,
            };

            await _s3Client.AbortMultipartUploadAsync(abortRequest, cancellationToken);
        }

        public async Task<string> MultipartUploadComplete(
            FileName fileName,
            string uploadId,
            List<Shared.DTOs.PartETag> partETags,
            CancellationToken cancellationToken)
        {
            var completeRequest = new CompleteMultipartUploadRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
                UploadId = uploadId,
                PartETags = partETags.Select(pt => new Amazon.S3.Model.PartETag(pt.PartNumber, pt.Value)).ToList(),
            };

            var response = await _s3Client.CompleteMultipartUploadAsync(completeRequest, cancellationToken);

            return response.Key;
        }

        public async Task<string> GetPresignedUrlToUploadChunkOfFile(
            FileName fileName,
            string uploadId,
            int partNumber)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(60),
                PartNumber = partNumber,
                UploadId = uploadId,
                Protocol = _minioOptions.IsWithSSL ? Protocol.HTTPS : Protocol.HTTP,
            };

            return await _s3Client.GetPreSignedURLAsync(request);
        }

        public async Task<string> DeleteFileFromFileService(
            FileName fileName,
            CancellationToken cancellationToken)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = fileName.BucketName,
                Key = fileName.Key,
            };

            await _s3Client.DeleteObjectAsync(request, cancellationToken);

            return fileName.Key;
        }
    }
}
