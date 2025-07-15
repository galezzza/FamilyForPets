using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public record GetPresignedUrlToUploadFullFileToFileServiceCommand(
        FileName FileName) : ICommand;
}
