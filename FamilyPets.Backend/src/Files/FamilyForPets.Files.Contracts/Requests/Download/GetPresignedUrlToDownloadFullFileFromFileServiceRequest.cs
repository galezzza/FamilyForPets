using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Download
{
    public record GetPresignedUrlToDownloadFullFileFromFileServiceRequest(
        FileName FileName);
}
