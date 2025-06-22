using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public class MulipartUploadCancelHandler
        : ICommandHandler<MulipartUploadCancelCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            MulipartUploadCancelCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
