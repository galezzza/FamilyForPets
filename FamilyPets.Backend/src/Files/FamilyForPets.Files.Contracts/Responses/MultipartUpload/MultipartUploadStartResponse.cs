using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.Contracts.Responses.MultipartUpload
{
    public record MultipartUploadStartResponse(
        FileName FileName,
        string UploadId,
        long ChunkSize,
        int TotalChunks);
}
