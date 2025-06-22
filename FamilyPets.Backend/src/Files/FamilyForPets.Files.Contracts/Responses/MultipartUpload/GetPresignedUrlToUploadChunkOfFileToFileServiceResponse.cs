namespace FamilyForPets.Files.Contracts.Responses.MultipartUpload
{
    public record GetPresignedUrlToUploadChunkOfFileToFileServiceResponse(
        string Url,
        int PartNumber);
}
