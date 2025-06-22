using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public class MultipartUploadStartCommandValidator
        : AbstractValidator<MultipartUploadStartCommand>
    {
        public MultipartUploadStartCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();

            RuleFor(c => c.FileSize)
                .GreaterThanOrEqualTo(0);
        }
    }
}
