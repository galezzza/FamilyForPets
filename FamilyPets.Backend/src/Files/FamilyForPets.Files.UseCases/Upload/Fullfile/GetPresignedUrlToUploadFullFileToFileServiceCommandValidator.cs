using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Fullfile
{
    public class GetPresignedUrlToUploadFullFileToFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToUploadFullFileToFileServiceCommand>
    {
        public GetPresignedUrlToUploadFullFileToFileServiceCommandValidator()
        {
        }
    }

}
