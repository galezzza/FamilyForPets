using CSharpFunctionalExtensions;
using FamilyForPets.Files.Contracts.Requests.Delete;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.Files.Contracts.Requests.Upload;
using FamilyForPets.Files.Contracts.Requests.Upload.Multipart;
using FamilyForPets.Files.Contracts.Responses.MultipartUpload;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.Contracts
{
    public interface IFilesContract
    {
        Task<UnitResult<ErrorList>> CancelMultipartUpload(MultipartUploadCancelRequest request, CancellationToken cancellationToken = default);

        Task<Result<string, ErrorList>> CompleteMultipartUpload(MultipartUploadCompleteRequest request, CancellationToken cancellationToken = default);

        Task<Result<Guid, ErrorList>> Delete(DeleteFileFromFileServiceRequest request, CancellationToken cancellationToken = default);

        Task<Result<string, ErrorList>> GetDownloadFileUrl(GetPresignedUrlToDownloadFullFileFromFileServiceRequest request, CancellationToken cancellationToken = default);

        Task<Result<MultipartUploadStartResponse, ErrorList>> GetStartUploadMultipartFileUrl(MultipartUploadStartRequest request, CancellationToken cancellationToken = default);

        Task<Result<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>> GetUploadChunkFileUrl(GetPresignedUrlToUploadChunkOfFileToFileServiceRequest request, CancellationToken cancellationToken = default);

        Task<Result<string, ErrorList>> GetUploadFullFileUrl(GetPresignedUrlToUploadFullFileToFileServiceRequest request, CancellationToken cancellationToken = default);

    }
}
