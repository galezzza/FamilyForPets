using FamilyForPets.Core.DTOs;
using FamilyForPets.Core.Validation;
using FamilyForPets.Files.Shared.DTOs;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Complete
{
    public class MultipartUploadCompleteCommandValidator
        : AbstractValidator<MultipartUploadCompleteCommand>
    {
        public MultipartUploadCompleteCommandValidator()
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

            RuleFor(c => c.ETags)
                .NotEmpty()
                .ForEach(validator =>
                validator.ChildRules(tag =>
                {
                    tag.RuleFor(t => t.Value)
                    .NotEmpty()
                    .WithError(Errors.General.CannotBeEmpty("ETag"));

                    tag.RuleFor(t => t.PartNumber)
                    .GreaterThanOrEqualTo(0)
                    .WithError(Errors.General.ValueIsInvalid("Part number"));
                }))
                .WithError(Errors.General.CannotBeEmpty(nameof(PartETag)));
        }
    }

}
