using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Delete
{

    public record DeleteFileFromFileServiceRequest(
        FileName FileName);
}
