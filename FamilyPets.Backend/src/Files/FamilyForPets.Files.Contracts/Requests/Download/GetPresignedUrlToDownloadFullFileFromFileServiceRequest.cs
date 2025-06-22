using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Download
{
    public record GetPresignedUrlToDownloadFullFileFromFileServiceRequest(
        FileName FileName);
}
