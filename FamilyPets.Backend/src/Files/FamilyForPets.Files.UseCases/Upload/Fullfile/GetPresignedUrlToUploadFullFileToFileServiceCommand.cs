using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public record GetPresignedUrlToUploadFullFileToFileServiceCommand(
        FileName FileName) : ICommand;
}
