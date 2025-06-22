using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Complete
{
    public class MultipartUploadCompleteHandler
        : ICommandHandler<MultipartUploadCompleteCommand, string>
    {
        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<MultipartUploadCompleteCommand> _validator;
        private readonly ILogger<MultipartUploadCompleteHandler> _logger;

        public MultipartUploadCompleteHandler(
            IFilesProvider filesProvider,
            IValidator<MultipartUploadCompleteCommand> validator,
            ILogger<MultipartUploadCompleteHandler> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> HandleAsync(
            MultipartUploadCompleteCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<string, ErrorList>(validationResult.ToErrorListFromValidationResult());

            string key = await _filesProvider.MultipartUploadComplete(
                command.FileName, command.UploadId, command.ETags, cancellationToken);

            return Result.Success<string, ErrorList>(key);

        }
    }
}
