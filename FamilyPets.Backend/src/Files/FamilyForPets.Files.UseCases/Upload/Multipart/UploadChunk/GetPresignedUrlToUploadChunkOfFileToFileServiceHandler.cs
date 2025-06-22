using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public class GetPresignedUrlToUploadChunkOfFileToFileServiceHandler
        : ICommandHandler<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
