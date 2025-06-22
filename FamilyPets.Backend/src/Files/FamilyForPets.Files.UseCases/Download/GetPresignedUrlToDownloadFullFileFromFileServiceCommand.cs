using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.Contracts.Requests.Download
{
    public record GetPresignedUrlToDownloadFullFileFromFileServiceCommand(
        FileName FileName) : ICommand;
}
