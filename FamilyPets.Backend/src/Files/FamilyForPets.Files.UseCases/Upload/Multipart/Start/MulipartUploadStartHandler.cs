using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public class MulipartUploadStartHandler
        : ICommandHandler<MulipartUploadStartCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            MulipartUploadStartCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
