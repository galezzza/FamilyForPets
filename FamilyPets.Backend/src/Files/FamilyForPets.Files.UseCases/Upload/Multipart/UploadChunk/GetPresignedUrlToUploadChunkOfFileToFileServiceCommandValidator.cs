using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public class GetPresignedUrlToUploadChunkOfFileToFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand>
    {
        public GetPresignedUrlToUploadChunkOfFileToFileServiceCommandValidator()
        {
        }
    }
}
