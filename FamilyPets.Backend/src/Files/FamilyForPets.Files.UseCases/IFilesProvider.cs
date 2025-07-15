using FamilyForPets.Files.Domain.DTOs;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.UseCases
{
    public interface IFilesProvider
    {
        Task<string> GetPresignedUrlToUploadFullFile(
            FileName fileName);

        Task<string> GetPresignedUrlToDownloadFullFile(
            FileName fileName);

        Task<string> MultipartUploadStart(
            FileName fileName,
            string contentType,
            CancellationToken cancellationToken);

        Task MultipartUploadCancel(
            FileName fileName,
            string uploadId,
            CancellationToken cancellationToken);

        Task<string> MultipartUploadComplete(
            FileName fileName, string uploadId,
            List<PartETag> partETags,
            CancellationToken cancellationToken);

        Task<string> GetPresignedUrlToUploadChunkOfFile(
            FileName fileName,
            string uploadId,
            int partNumber);

        Task<string> DeleteFileFromFileService(
            FileName fileName,
            CancellationToken cancellationToken);

    }
}