using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.Start
{
    public class MulipartUploadStartCommandValidator
        : AbstractValidator<MulipartUploadStartCommand>
    {
        public MulipartUploadStartCommandValidator()
        {
        }
    }
}
