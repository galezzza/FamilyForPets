using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record GetPresignedUrlToUploadChunkOfFileToFileServiceRequest(
        FileName FileName,
        string UploadId,
        int PartNumber);

}
