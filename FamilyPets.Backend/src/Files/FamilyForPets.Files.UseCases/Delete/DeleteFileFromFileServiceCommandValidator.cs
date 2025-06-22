using FluentValidation;

namespace FamilyForPets.Files.UseCases.Delete
{
    public class DeleteFileFromFileServiceCommandValidator : AbstractValidator<DeleteFileFromFileServiceCommand>
    {
        public DeleteFileFromFileServiceCommandValidator() { }
    }
}
