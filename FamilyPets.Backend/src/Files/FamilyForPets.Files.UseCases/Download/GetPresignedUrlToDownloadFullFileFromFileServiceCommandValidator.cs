using FluentValidation;

namespace FamilyForPets.Files.UseCases.Download
{
    public class GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToDownloadFullFileFromFileServiceCommand>
    {
        public GetPresignedUrlToDownloadFullFileFromFileServiceCommandValidator()
        {
        }
    }
}
