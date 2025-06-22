using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public record MulipartUploadCancelCommand(
        FileName FileName,
        string UploadId) : ICommand;
}
