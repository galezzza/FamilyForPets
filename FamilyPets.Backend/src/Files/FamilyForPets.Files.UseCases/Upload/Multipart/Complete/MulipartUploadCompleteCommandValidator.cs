using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Complete
{
    public class MulipartUploadCompleteCommandValidator
        : AbstractValidator<MulipartUploadCompleteCommand>
    {
        public MulipartUploadCompleteCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();

            RuleFor(c => c.UploadId)
                .NotEmpty();

            RuleFor(c => c.ETags)
                .NotEmpty()
                .ForEach(validator =>
                validator.ChildRules(tag =>
                {
                    tag.RuleFor(t => t.Value).NotEmpty();
                }));
        }
    }

}
