using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public class MulipartUploadStartCommandValidator
        : AbstractValidator<MulipartUploadStartCommand>
    {
        public MulipartUploadStartCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();
        }
    }
}
