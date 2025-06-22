using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Download
{
    public class GetPresignedUrlToDownloadFullFileFromFileServiceHandler()
        : ICommandHandler<GetPresignedUrlToDownloadFullFileFromFileServiceCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            GetPresignedUrlToDownloadFullFileFromFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
