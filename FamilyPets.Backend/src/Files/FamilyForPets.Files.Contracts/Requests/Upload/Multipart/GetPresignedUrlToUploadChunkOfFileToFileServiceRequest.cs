using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record GetPresignedUrlToUploadChunkOfFileToFileServiceRequest(
        FileName FileName,
        string UploadId,
        int PartNumber);

}
