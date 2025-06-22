using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Delete
{
    public class DeleteFileFromFileServiceHandler
        : ICommandHandler<DeleteFileFromFileServiceCommand>
    {
        public async Task<UnitResult<ErrorList>> HandleAsync(
            DeleteFileFromFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            return UnitResult.Success<ErrorList>();
        }
    }
}
