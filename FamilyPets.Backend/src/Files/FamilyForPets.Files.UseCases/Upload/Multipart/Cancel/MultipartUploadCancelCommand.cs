using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public record MultipartUploadCancelCommand(
        FileName FileName,
        string UploadId) : ICommand;
}
