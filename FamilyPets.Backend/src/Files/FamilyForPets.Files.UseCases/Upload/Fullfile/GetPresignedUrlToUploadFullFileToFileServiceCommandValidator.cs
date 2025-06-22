using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public class GetPresignedUrlToUploadFullFileToFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToUploadFullFileToFileServiceCommand>
    {
        public GetPresignedUrlToUploadFullFileToFileServiceCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();
        }
    }

}
