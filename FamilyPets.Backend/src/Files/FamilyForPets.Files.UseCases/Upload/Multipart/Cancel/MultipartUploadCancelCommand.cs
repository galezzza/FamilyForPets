using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public record MultipartUploadCancelCommand(
        FileName FileName,
        string UploadId) : ICommand;
}
