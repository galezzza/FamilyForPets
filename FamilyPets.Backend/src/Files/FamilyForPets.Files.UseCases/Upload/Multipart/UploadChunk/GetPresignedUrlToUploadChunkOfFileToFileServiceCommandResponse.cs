using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public record GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse(
        string Url,
        int PartNumber);
}
