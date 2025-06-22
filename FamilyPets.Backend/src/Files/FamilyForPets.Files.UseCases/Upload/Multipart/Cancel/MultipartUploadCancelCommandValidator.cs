using FamilyForPets.Core.Validation;
using FamilyForPets.Files.Shared.DTOs;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public class MultipartUploadCancelCommandValidator
        : AbstractValidator<MultipartUploadCancelCommand>
    {
        public MultipartUploadCancelCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.Key)));

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.BucketName)));

            RuleFor(c => c.UploadId)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty("Upload Id"));

        }
    }
}
