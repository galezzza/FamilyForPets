using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases
{
    public interface IFilesProvider
    {
        Task<string> GetPresignedUrlToUploadFullFileToFileService(
            FileName fileName);

        Task<string> GetPresignedUrlToDownloadFullFileFromFileService(
            FileName fileName);

        Task<string> MultipartUploadStart(
            FileName fileName,
            CancellationToken cancellationToken);

        Task MultipartUploadCancel(
            FileName fileName,
            string uploadId,
            CancellationToken cancellationToken);

        Task<string> MultipartUploadComplete(
            FileName fileName, string uploadId,
            List<PartETag> partETags,
            CancellationToken cancellationToken);

        Task<string> GetPresignedUrlToUploadChunkOfFileToFileService(
            FileName fileName,
            string uploadId,
            int partNumber);

        Task<string> DeleteFileFromFileService(
            FileName fileName,
            CancellationToken cancellationToken);

    }
}