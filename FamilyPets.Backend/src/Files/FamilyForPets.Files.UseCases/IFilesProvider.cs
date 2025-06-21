namespace FamilyForPets.Files.UseCases
{

    public interface IFilesProvider
    {
        Task GetPresignedUrlToUploadFullFileToFileService();

        Task GetPresignedUrlToDownloadFullFileFromFileService();

        Task MultipartUploadStart();

        Task MultipartUploadCancel();

        Task MultipartUploadComplete();

        Task GetPresignedUrlToUploadChunkOfFileToFileService();

        Task DeleteFileFromFileService();

    }
}