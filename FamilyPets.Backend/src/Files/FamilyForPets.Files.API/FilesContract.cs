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
        private readonly DeleteFileFromFileServiceHandler _handler1;
        private readonly GetPresignedUrlToUploadFullFileToFileServiceHandler _handler2;
        private readonly GetPresignedUrlToDownloadFullFileFromFileServiceHandler _handler3;
        private readonly MultipartUploadStartHandler _handler4;
        private readonly MultipartUploadCancelHandler _handler5;
        private readonly MultipartUploadCompleteHandler _handler6;
        private readonly GetPresignedUrlToUploadChunkOfFileToFileServiceHandler _handler7;

        public FilesContract(
            DeleteFileFromFileServiceHandler handler1,
            GetPresignedUrlToUploadFullFileToFileServiceHandler handler2,
            GetPresignedUrlToDownloadFullFileFromFileServiceHandler handler3,
            MultipartUploadStartHandler handler4,
            MultipartUploadCancelHandler handler5,
            MultipartUploadCompleteHandler handler6,
            GetPresignedUrlToUploadChunkOfFileToFileServiceHandler handler7)
        {
            _handler1 = handler1;
            _handler2 = handler2;
            _handler3 = handler3;
            _handler4 = handler4;
            _handler5 = handler5;
            _handler6 = handler6;
            _handler7 = handler7;
        }

        public async Task<Result<Guid, ErrorList>> Delete(
            DeleteFileFromFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            DeleteFileFromFileServiceCommand command = new(request.FileName);
            return await _handler1.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<string, ErrorList>> GetUploadFullFileUrl(
            GetPresignedUrlToUploadFullFileToFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadFullFileToFileServiceCommand command = new(request.FileName);
            return await _handler2.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<string, ErrorList>> GetDownloadFileUrl(
            GetPresignedUrlToDownloadFullFileFromFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToDownloadFullFileFromFileServiceCommand command = new(
                request.FileName);
            return await _handler3.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<MultipartUploadStartResponse, ErrorList>> GetStartUploadMultipartFileUrl(
            MultipartUploadStartRequest request,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadStartCommand command = new(request.FileName, request.FileSize);
            Result<MultipartUploadStartCommandResponse, ErrorList> result = await _handler4
                .HandleAsync(command, cancellationToken);
            if (result.IsFailure)
            {
                return Result.Failure<
                    MultipartUploadStartResponse, ErrorList>(result.Error);
            }

            MultipartUploadStartResponse response = new(
                result.Value.FileName,
                result.Value.UploadId,
                result.Value.ChunkSize,
                result.Value.TotalChunks);
            return Result.Success<MultipartUploadStartResponse, ErrorList>(response);
        }

        public async Task<UnitResult<ErrorList>> CancelMultipartUpload(
            MultipartUploadCancelRequest request,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadCancelCommand command = new(
                request.FileName, request.UploadId);
            return await _handler5.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<string, ErrorList>> CompleteMultipartUpload(
            MultipartUploadCompleteRequest request,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadCompleteCommand command = new(
                request.FileName, request.UploadId, request.ETags);
            return await _handler6.HandleAsync(command, cancellationToken);
        }

        public async Task<Result<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>> GetUploadChunkFileUrl(
            GetPresignedUrlToUploadChunkOfFileToFileServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command = new(
                request.FileName,
                request.UploadId,
                request.PartNumber);

            Result<GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse, ErrorList> result
                = await _handler7.HandleAsync(command, cancellationToken);
            if (result.IsFailure)
            {
                return Result.Failure<
                    GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>(
                    result.Error);
            }

            GetPresignedUrlToUploadChunkOfFileToFileServiceResponse response = new(
                result.Value.Url,
                result.Value.PartNumber);

            return Result.Success<
                GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList>(
                response);
        }
    }
}
