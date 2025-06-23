using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public record GetPresignedUrlToUploadFullFileToFileServiceCommand(
        FileName FileName) : ICommand;
}
