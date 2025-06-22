using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Responses.MultipartUpload
{
    public record MultipartUploadStartResponse(
        FileName FileName,
        string UploadId,
        long ChunkSize,
        int TotalChunks);
}
