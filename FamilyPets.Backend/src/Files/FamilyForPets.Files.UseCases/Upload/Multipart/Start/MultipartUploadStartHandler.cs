using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public class MultipartUploadStartHandler
        : ICommandHandler<MultipartUploadStartCommand, MultipartUploadStartCommandResponse>
    {
        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<MultipartUploadStartCommand> _validator;
        private readonly ILogger<MultipartUploadStartHandler> _logger;

        public MultipartUploadStartHandler(
            IFilesProvider filesProvider,
            IValidator<MultipartUploadStartCommand> validator,
            ILogger<MultipartUploadStartHandler> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<MultipartUploadStartCommandResponse, ErrorList>> HandleAsync(
            MultipartUploadStartCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<MultipartUploadStartCommandResponse, ErrorList>(
                    validationResult.ToErrorListFromValidationResult());
            }

            string uploadId = await _filesProvider.MultipartUploadStart(
                command.FileName, cancellationToken);

            (long chunkSize, int totalChunks) = ChunkSizeCalculator.Calculate(command.FileSize);

            MultipartUploadStartCommandResponse response = new(
                command.FileName,
                uploadId,
                chunkSize,
                totalChunks);
            return Result.Success<MultipartUploadStartCommandResponse, ErrorList>(response);
        }
    }
}
