using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public class GetPresignedUrlToUploadChunkOfFileToFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand>
    {
        public GetPresignedUrlToUploadChunkOfFileToFileServiceCommandValidator()
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

            RuleFor(c => c.PartNumber)
                .GreaterThan(0)
                .WithError(Errors.General.ValueIsInvalid("Part Number"));
        }
    }
}
