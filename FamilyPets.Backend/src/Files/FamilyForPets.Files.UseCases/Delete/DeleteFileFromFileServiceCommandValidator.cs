using FluentValidation;

namespace FamilyForPets.Files.UseCases.Delete
{
    public class DeleteFileFromFileServiceCommandValidator : AbstractValidator<DeleteFileFromFileServiceCommand>
    {
        public DeleteFileFromFileServiceCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();
        }
    }
}
