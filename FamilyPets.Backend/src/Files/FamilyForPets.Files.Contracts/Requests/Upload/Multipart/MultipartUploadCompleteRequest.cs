using FamilyForPets.Files.Domain.DTOs;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.Contracts.Requests.Upload.Multipart
{
    public record MultipartUploadCompleteRequest(
        FileName FileName,
        string UploadId,
        List<PartETag> ETags);

}
