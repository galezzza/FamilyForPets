using FamilyForPets.Core.DTOs;
using FamilyForPets.Core.Validation;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Download
{
    public class GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToDownloadFullFileFromFileServiceCommand>
    {
        public GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator()
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
