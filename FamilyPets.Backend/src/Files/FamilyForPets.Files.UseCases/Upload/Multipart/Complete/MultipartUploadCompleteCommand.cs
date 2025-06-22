using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Complete
{
    public record MultipartUploadCompleteCommand(
        FileName FileName,
        string UploadId,
        List<PartETag> ETags) : ICommand;
}
