using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Contracts.Requests.Delete;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.Files.Contracts.Requests.Upload;
using FamilyForPets.Files.Contracts.Requests.Upload.Multipart;
using FamilyForPets.Files.Contracts.Responses.MultipartUpload;
using FamilyForPets.Files.Infrastructure.Options;
using FamilyForPets.Files.UseCases.Delete;
using FamilyForPets.Files.UseCases.Upload.Fullfile;
using FamilyForPets.Files.UseCases.Upload.Multipart.Cancel;
using FamilyForPets.Files.UseCases.Upload.Multipart.Complete;
using FamilyForPets.Files.UseCases.Upload.Multipart.Start;
using FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk;
using FamilyForPets.Framework.Responses.EndpointResults;
using FamilyForPets.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FamilyForPets.Files.API
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly MinioOptions _options;

        public FilesController(IOptionsMonitor<MinioOptions> options)
        {
            _options = options.CurrentValue;
        }

        [HttpDelete]
        public async Task<EndpointResult<Guid>> Delete(
            [FromBody] DeleteFileFromFileServiceRequest request,
            [FromServices] ICommandHandler<DeleteFileFromFileServiceCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            DeleteFileFromFileServiceCommand command = new(request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("uploading")]
        public async Task<EndpointResult<string>> GetUploadFullFileUrl(
            [FromBody] GetPresignedUrlToUploadFullFileToFileServiceRequest request,
            [FromServices] ICommandHandler<
                GetPresignedUrlToUploadFullFileToFileServiceCommand, string> handler,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadFullFileToFileServiceCommand command = new(request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("downloading")]
        public async Task<EndpointResult<string>> GetDownloadFileUrl(
            [FromBody] GetPresignedUrlToDownloadFullFileFromFileServiceRequest request,
            [FromServices] ICommandHandler<
                GetPresignedUrlToDownloadFullFileFromFileServiceCommand, string> handler,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToDownloadFullFileFromFileServiceCommand command = new(
                request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("multipart/starting")]
        public async Task<EndpointResult<MultipartUploadStartResponse>> GetStartUploadMultipartFileUrl(
            [FromBody] MultipartUploadStartRequest request,
            [FromServices] ICommandHandler<MultipartUploadStartCommand, MultipartUploadStartCommandResponse> handler,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadStartCommand command = new(request.FileName, request.FileSize);
            Result<MultipartUploadStartCommandResponse, ErrorList> result = await handler
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

        [HttpPost("multipart/canceling")]
        public async Task<EndpointResult> CancelMultipartUpload(
            [FromBody] MultipartUploadCancelRequest request,
            [FromServices] ICommandHandler<MultipartUploadCancelCommand> handler,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadCancelCommand command = new(
                request.FileName, request.UploadId);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("multipart/ending")]
        public async Task<EndpointResult<string>> CompleteMultipartUpload(
            [FromBody] MultipartUploadCompleteRequest request,
            [FromServices] ICommandHandler<MultipartUploadCompleteCommand, string> handler,
            CancellationToken cancellationToken = default)
        {
            MultipartUploadCompleteCommand command = new(
                request.FileName, request.UploadId, request.ETags);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("miltipart/chunk/uploading")]
        public async Task<EndpointResult<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse>> GetUploadChunkFileUrl(
            [FromBody] GetPresignedUrlToUploadChunkOfFileToFileServiceRequest request,
            [FromServices] ICommandHandler<
                GetPresignedUrlToUploadChunkOfFileToFileServiceCommand,
                GetPresignedUrlToUploadChunkOfFileToFileServiceResponse> handler,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command = new(
                request.FileName,
                request.UploadId,
                request.PartNumber);

            Result<GetPresignedUrlToUploadChunkOfFileToFileServiceResponse, ErrorList> result
                = await handler.HandleAsync(command, cancellationToken);
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
