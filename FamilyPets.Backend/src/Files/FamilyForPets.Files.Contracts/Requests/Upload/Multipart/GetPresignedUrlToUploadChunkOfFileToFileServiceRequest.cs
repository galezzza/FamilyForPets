using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record GetPresignedUrlToUploadChunkOfFileToFileServiceRequest(
        FileName FileName,
        string UploadId,
        int PartNumber);

}
