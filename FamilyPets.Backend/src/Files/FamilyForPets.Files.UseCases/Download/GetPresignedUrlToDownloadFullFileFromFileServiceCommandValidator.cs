using FamilyForPets.Files.Contracts.Requests.Download;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Download
{
    public class GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToDownloadFullFileFromFileServiceCommand>
    {
        public GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();
        }
    }
}
