using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public class MultipartUploadCancelHandler
        : ICommandHandler<MultipartUploadCancelCommand>
    {
        private readonly IFilesProvider _filesProvider;
        private readonly IValidator<MultipartUploadCancelCommand> _validator;
        private readonly ILogger<MultipartUploadCancelCommand> _logger;

        public MultipartUploadCancelHandler(
            IFilesProvider filesProvider,
            IValidator<MultipartUploadCancelCommand> validator,
            ILogger<MultipartUploadCancelCommand> logger)
        {
            _filesProvider = filesProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<UnitResult<ErrorList>> HandleAsync(
            MultipartUploadCancelCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return UnitResult.Failure<ErrorList>(validationResult.ToErrorListFromValidationResult());

            await _filesProvider.MultipartUploadCancel();

            return UnitResult.Success<ErrorList>();

        }
    }
}
