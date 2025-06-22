using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Cancel
{
    public class MulipartUploadCancelCommandValidator
        : AbstractValidator<MulipartUploadCancelCommand>
    {
        public MulipartUploadCancelCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();

            RuleFor(c => c.UploadId)
                .NotEmpty();

        }
    }
}
