using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.Contracts.Requests.Download
{
    public record GetPresignedUrlToDownloadFullFileFromFileServiceRequest(
        FileName FileName);
}
