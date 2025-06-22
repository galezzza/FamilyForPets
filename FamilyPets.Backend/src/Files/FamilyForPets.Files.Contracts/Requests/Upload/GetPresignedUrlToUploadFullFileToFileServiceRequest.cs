using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Upload
{
    public record GetPresignedUrlToUploadFullFileToFileServiceRequest(
        FileName FileName);

}
