using FamilyForPets.Core.Validation;
using FamilyForPets.Files.Shared.DTOs;
using FamilyForPets.SharedKernel;
using FluentValidation;

namespace FamilyForPets.Files.UseCases.Delete
{
    public class DeleteFileFromFileServiceCommandValidator : AbstractValidator<DeleteFileFromFileServiceCommand>
    {
        public DeleteFileFromFileServiceCommandValidator()
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
