using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public record MulipartUploadStartCommand(
        FileName FileName) : ICommand;
}
