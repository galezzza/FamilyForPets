using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Contracts;
using FamilyForPets.Files.Contracts.Requests.Delete;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.Files.Contracts.Requests.Upload;
using FamilyForPets.Files.Contracts.Requests.Upload.Multipart;
using FamilyForPets.Files.Contracts.Responses.MultipartUpload;
using FamilyForPets.Files.UseCases.Delete;
using FamilyForPets.Files.UseCases.Download;
using FamilyForPets.Files.UseCases.Upload.Fullfile;
using FamilyForPets.Files.UseCases.Upload.Multipart.Cancel;
using FamilyForPets.Files.UseCases.Upload.Multipart.Complete;
using FamilyForPets.Files.UseCases.Upload.Multipart.Start;
using FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk;
using FamilyForPets.SharedKernel;

namespace FamilyForPets.Files.API
{
    public class FilesContract : IFilesContract
    {
        private readonly DeleteFileFromFileServiceHandler _deleteFileHandler;
        private readonly GetPresignedUrlToUploadFullFileToFileServiceHandler _uploadFullFileHandler;
        private readonly GetPresignedUrlToDownloadFullFileFromFileServiceHandler _downloadFullFileHandler;
        private readonly MultipartUploadStartHandler _multipartUploadStartHandler;
        private readonly MultipartUploadCancelHandler _multipartUploadCancelHandler;
        private readonly MultipartUploadCompleteHandler _multipartUploadCompleteHandler;
        private readonly GetPresignedUrlToUploadChunkOfFileToFileServiceHandler _uploadChunkOfFileHandler;

        public FilesContract(
            DeleteFileFromFileServiceHandler deleteFileHandler,
            GetPresignedUrlToUploadFullFileToFileServiceHandler uploadFullFileHandler,
            GetPresignedUrlToDownloadFullFileFromFileServiceHandler downloadFullFileHandler,
            MultipartUploadStartHandler multipartUploadStartHandler,
            MultipartUploadCancelHandler multipartUploadCancelHandler,
            MultipartUploadCompleteHandler multipartUploadCompleteHandler,
            GetPresignedUrlToUploadChunkOfFileToFileServiceHandler uploadChunkOfFileHandlerndler7)
        {
            _deleteFileHandler = deleteFileHandler;
            _uploadFullFileHandler = uploadFullFileHandler;
            _downloadFullFileHandler = downloadFullFileHandler;
            _multipartUploadStartHandler = multipartUploadStartHandler;
            _multipartUploadCancelHandler = multipartUploadCancelHandler;
            _multipartUploadCompleteHandler = multipartUploadCompleteHandler;
            _uploadChunkOfFileHandler = uploadChunkOfFileHandlerndler7;
        }

        public async Task<Result<Guid, ErrorList>> Delete(
            DeleteFileFromFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            DeleteFileFromFileServiceCommand command = new(request.FileName);
            return await _deleteFileHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<string, ErrorList>> GetUploadFullFileUrl(
            GetPresignedUrlToUploadFullFileToFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadFullFileToFileServiceCommand command = new(request.FileName);
            return await _uploadFullFileHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<string, ErrorList>> GetDownloadFileUrl(
            GetPresignedUrlToDownloadFullFileFromFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToDownloadFullFileFromFileServiceCommand command = new(
                request.FileName);
            return await _downloadFullFileHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<MultipartUploadStartResponse, ErrorList>> GetStartUploadMultipartFileUrl(
            MultipartUploadStartRequest request,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadStartCommand command = new(request.FileName, request.ContentType, request.FileSize);
            Result<MultipartUploadStartResponse, ErrorList> result = await _multipartUploadStartHandler
                .HandleAsync(command, cancellationToken);
            if (result.IsFailure)
                return Result.Failure<MultipartUploadStartResponse, ErrorList>(result.Error);

            return Result.Success<MultipartUploadStartResponse, ErrorList>(result.Value);
        }

        public async Task<UnitResult<ErrorList>> CancelMultipartUpload(
            MultipartUploadCancelRequest request,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadCancelCommand command = new(
                request.FileName, request.UploadId);
            return await _multipartUploadCancelHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<string, ErrorList>> CompleteMultipartUpload(
            MultipartUploadCompleteRequest request,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadCompleteCommand command = new(
                request.FileName, request.UploadId, request.ETags);
            return await _multipartUploadCompleteHandler.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>> GetUploadChunkFileUrl(
            GetPresignedUrlToUploadChunkOfFileToFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command = new(
                request.FileName,
                request.UploadId,
                request.PartNumber);

            Result<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList> result
                = await _uploadChunkOfFileHandler.HandleAsync(command, cancellationToken);
            if (result.IsFailure)
            {
                return Result.Failure<
                    GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>(
                    result.Error);
            }

            return Result.Success<
                GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>(result.Value);
        }
    }
}
