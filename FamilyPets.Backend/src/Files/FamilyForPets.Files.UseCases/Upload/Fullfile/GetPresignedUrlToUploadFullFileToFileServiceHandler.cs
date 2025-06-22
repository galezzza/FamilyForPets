using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public class GetPresignedUrlToUploadFullFileToFileServiceHandler
        : ICommandHandler<GetPresignedUrlToUploadFullFileToFileServiceCommand, string>
    {
        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<GetPresignedUrlToUploadFullFileToFileServiceCommand> _validator;
        private readonly ILogger<GetPresignedUrlToUploadFullFileToFileServiceHandler> _logger;

        public GetPresignedUrlToUploadFullFileToFileServiceHandler(
            IFilesProvider filesProvider,
            IValidator<GetPresignedUrlToUploadFullFileToFileServiceCommand> validator,
            ILogger<GetPresignedUrlToUploadFullFileToFileServiceHandler> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> HandleAsync(
            GetPresignedUrlToUploadFullFileToFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<string, ErrorList>(validationResult.ToErrorListFromValidationResult());

            await _filesProvider.GetPresignedUrlToUploadFullFileToFileService();

            string result = command.FileName.BucketName;

            return Result.Success<string, ErrorList>(result);

        }
    }
}
