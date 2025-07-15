using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.UseCases.Download
{
    public record GetPresignedUrlToDownloadFullFileFromFileServiceCommand(
        FileName FileName) : ICommand;
}
