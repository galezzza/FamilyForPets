using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Delete
{
    public class DeleteFileFromFileServiceHandler
        : ICommandHandler<DeleteFileFromFileServiceCommand, Guid>
    {
        private readonly ILogger<DeleteFileFromFileServiceHandler> _logger;
        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<DeleteFileFromFileServiceCommand> _validator;

        public DeleteFileFromFileServiceHandler(
            IFilesProvider filesProvider,
            IValidator<DeleteFileFromFileServiceCommand> validator,
            ILogger<DeleteFileFromFileServiceHandler> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            DeleteFileFromFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            string deletedId = await _filesProvider.DeleteFileFromFileService(command.FileName, cancellationToken);
            if (string.IsNullOrWhiteSpace(deletedId))
            {
                return Result.Failure<Guid, ErrorList>(Errors.General.NotFound(
                    new(nameof(command.FileName.Key), command.FileName.Key)).ToErrorList());
            }

            Guid result = Guid.Parse(deletedId);

            return Result.Success<Guid, ErrorList>(result);
        }
    }
}
