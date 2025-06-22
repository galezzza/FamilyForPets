using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record MultipartUploadCancelRequest(
        FileName FileName,
        string UploadId);

}
