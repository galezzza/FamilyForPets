using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.Files.Contracts.Responses.MultipartUpload;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public class GetPresignedUrlToUploadChunkOfFileToFileServiceHandler
        : ICommandHandler<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand,
            GetPresignedUrlToUploadChunkOfFileToFileServiceResponse>
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

        public async Task<Result<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>> HandleAsync(
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse,
                    ErrorList>(validationResult.ToErrorListFromValidationResult());
            }

            string uploadUrl = await _filesProvider
                .GetPresignedUrlToUploadChunkOfFile(
                    command.FileName, command.UploadId, command.PartNumber);

            return Result.Success<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse,
                ErrorList>(new(uploadUrl, command.PartNumber));
        }
    }
}
