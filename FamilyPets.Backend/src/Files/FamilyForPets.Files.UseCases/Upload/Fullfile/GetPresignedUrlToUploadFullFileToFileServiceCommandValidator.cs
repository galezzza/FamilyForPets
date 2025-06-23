using FamilyForPets.Core.DTOs;
using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public class GetPresignedUrlToUploadFullFileToFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToUploadFullFileToFileServiceCommand>
    {
        public GetPresignedUrlToUploadFullFileToFileServiceCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.Key)));

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty()
                .WithError(Errors.General.CannotBeEmpty(nameof(FileName.BucketName)));
        }
    }

}
