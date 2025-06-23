using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload
{
    public record GetPresignedUrlToUploadFullFileToFileServiceRequest(
        FileName FileName);

}
