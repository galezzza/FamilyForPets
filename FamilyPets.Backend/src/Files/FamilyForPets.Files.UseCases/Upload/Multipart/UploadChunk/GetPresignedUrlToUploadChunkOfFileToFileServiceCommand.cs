using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public record GetPresignedUrlToUploadChunkOfFileToFileServiceCommand(
        FileName FileName,
        string UploadId,
        int PartNumber) : ICommand;
}
