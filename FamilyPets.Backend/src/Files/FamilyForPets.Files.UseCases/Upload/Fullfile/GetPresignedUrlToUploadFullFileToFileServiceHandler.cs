using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public class GetPresignedUrlToUploadFullFileToFileServiceHandler
        : ICommandHandler<GetPresignedUrlToUploadFullFileToFileServiceCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            GetPresignedUrlToUploadFullFileToFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
