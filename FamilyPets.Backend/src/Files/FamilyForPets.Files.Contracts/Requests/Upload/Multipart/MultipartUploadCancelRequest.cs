using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record MultipartUploadCancelRequest(
        FileName FileName,
        string UploadId);

}
