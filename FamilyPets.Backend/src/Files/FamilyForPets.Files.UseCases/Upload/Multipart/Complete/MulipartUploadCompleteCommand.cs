using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Complete
{
    public record MulipartUploadCompleteCommand(
        FileName FileName,
        string UploadId,
        List<ETag> ETags) : ICommand;
}
