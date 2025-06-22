using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Complete
{
    public class MulipartUploadCompleteHandler
        : ICommandHandler<MulipartUploadCompleteCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            MulipartUploadCompleteCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
