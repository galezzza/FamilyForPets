using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.UseCases.Test
{
    public class TestHandler : ICommandHandler<TestCommand, Guid>
    {
        private readonly IFilesProvider _filesProvider;

        public TestHandler(IFilesProvider filesProvider)
        {
            _filesProvider = filesProvider;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            TestCommand command,
            CancellationToken cancellationToken)
        {
            Guid result = _filesProvider.Test();
            return Result.Success<Guid, ErrorList>(result);
        }
    }
}
