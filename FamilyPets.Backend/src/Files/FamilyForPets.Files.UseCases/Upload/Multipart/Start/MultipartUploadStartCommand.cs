using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public record MultipartUploadStartCommand(
        FileName FileName,
        string ContentType,
        long FileSize) : ICommand;
}
