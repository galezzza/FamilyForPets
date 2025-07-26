using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.Contracts.Requests.Delete
{

    public record DeleteFileFromFileServiceRequest(
        FileName FileName);
}
