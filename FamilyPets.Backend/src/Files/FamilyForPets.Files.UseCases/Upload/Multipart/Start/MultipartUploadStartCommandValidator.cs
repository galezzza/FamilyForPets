using FamilyForPets.Core.DTOs;
using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public class MultipartUploadStartCommandValidator
        : AbstractValidator<MultipartUploadStartCommand>
    {
        public MultipartUploadStartCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.Key)));

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.BucketName)));

            RuleFor(c => c.FileSize)
                .GreaterThanOrEqualTo(0)
                .WithError(Errors.General.ValueIsInvalid("File Size"));
        }
    }
}
