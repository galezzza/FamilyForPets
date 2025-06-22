using FluentValidation;

namespace FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk
{
    public class GetPresignedUrlToUploadChunkOfFileToFileServiceCommandValidator
        : AbstractValidator<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand>
    {
        public GetPresignedUrlToUploadChunkOfFileToFileServiceCommandValidator()
        {
            RuleFor(c => c.FileName.Key)
                .NotEmpty();

            RuleFor(c => c.FileName.BucketName)
                .NotEmpty();

            RuleFor(c => c.UploadId)
                .NotEmpty();

            RuleFor(c => c.PartNumber)
                .GreaterThan(0);
        }
    }
}
