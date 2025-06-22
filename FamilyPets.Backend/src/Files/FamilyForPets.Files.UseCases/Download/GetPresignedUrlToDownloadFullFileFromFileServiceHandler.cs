using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.Files.UseCases.Delete;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Download
{
    public class GetPresignedUrlToDownloadFullFileFromFileServiceHandler
        : ICommandHandler<GetPresignedUrlToDownloadFullFileFromFileServiceCommand, string>
    {

        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<GetPresignedUrlToDownloadFullFileFromFileServiceCommand> _validator;
        private readonly ILogger<GetPresignedUrlToDownloadFullFileFromFileServiceHandler> _logger;

        public GetPresignedUrlToDownloadFullFileFromFileServiceHandler(
            IFilesProvider filesProvider,
            IValidator<GetPresignedUrlToDownloadFullFileFromFileServiceCommand> validator,
            ILogger<GetPresignedUrlToDownloadFullFileFromFileServiceHandler> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> HandleAsync(
            GetPresignedUrlToDownloadFullFileFromFileServiceCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<string, ErrorList>(validationResult.ToErrorListFromValidationResult());

            string downloadUrl = await _filesProvider
                .GetPresignedUrlToDownloadFullFileFromFileService(command.FileName);

            return Result.Success<string, ErrorList>(downloadUrl);

        }
    }
}
