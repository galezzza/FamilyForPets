using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public class GetPresignedUrlToUploadChunkOfFileToFileServiceHandler
        : ICommandHandler<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand,
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse>
    {
        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand> _validator;
        private readonly ILogger<GetPresignedUrlToUploadChunkOfFileToFileServiceHandler> _logger;

        public GetPresignedUrlToUploadChunkOfFileToFileServiceHandler(
            IFilesProvider filesProvider,
            IValidator<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand> validator,
            ILogger<GetPresignedUrlToUploadChunkOfFileToFileServiceHandler> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse, ErrorList>> HandleAsync(
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse,
                    ErrorList>(validationResult.ToErrorListFromValidationResult());
            }

            string uploadUrl = await _filesProvider
                .GetPresignedUrlToUploadChunkOfFileToFileService(
                    command.FileName, command.UploadId, command.PartNumber);

            GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse response = new(
                uploadUrl,
                command.PartNumber);
            return Result.Success<GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse,
                ErrorList>(response);
        }
    }
}
