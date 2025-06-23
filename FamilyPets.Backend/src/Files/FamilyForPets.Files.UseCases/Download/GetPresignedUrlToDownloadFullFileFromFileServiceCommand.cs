using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Download
{
    public record GetPresignedUrlToDownloadFullFileFromFileServiceCommand(
        FileName FileName) : ICommand;
}
