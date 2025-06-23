using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public record MultipartUploadStartCommand(
        FileName FileName,
        string ContentType,
        long FileSize) : ICommand;
}
