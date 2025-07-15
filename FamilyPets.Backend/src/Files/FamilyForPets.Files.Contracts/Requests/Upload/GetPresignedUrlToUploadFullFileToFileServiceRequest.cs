using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.Contracts.Requests.Upload
{
    public record GetPresignedUrlToUploadFullFileToFileServiceRequest(
        FileName FileName);

}
