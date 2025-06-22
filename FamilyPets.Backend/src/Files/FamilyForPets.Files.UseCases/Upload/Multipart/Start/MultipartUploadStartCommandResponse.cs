using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public record MultipartUploadStartCommandResponse(
        FileName FileName,
        string UploadId,
        long ChunkSize,
        int TotalChunks);
}
