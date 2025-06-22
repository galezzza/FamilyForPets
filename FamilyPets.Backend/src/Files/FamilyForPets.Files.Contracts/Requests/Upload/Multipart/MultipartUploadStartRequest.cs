using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record MultipartUploadStartRequest(
        FileName FileName,
        long FileSize);
}
