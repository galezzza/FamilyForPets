using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Contracts.Requests.Delete;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.Files.Contracts.Requests.Upload;
using FamilyForPets.Files.Contracts.Requests.Upload.Multipart;
using FamilyForPets.Files.Infrastructure.Options;
using FamilyForPets.Files.UseCases.Delete;
using FamilyForPets.Files.UseCases.Upload.Fullfile;
using FamilyForPets.Files.UseCases.Upload.Multipart.Cancel;
using FamilyForPets.Files.UseCases.Upload.Multipart.Complete;
using FamilyForPets.Files.UseCases.Upload.Multipart.Start;
using FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk;
using FamilyForPets.Framework.Responses.EndpointResults;
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

        [HttpDelete("files")]
        public async Task<EndpointResult<Guid>> Delete(
            [FromBody] DeleteFileFromFileServiceRequest request,
            [FromServices] ICommandHandler<DeleteFileFromFileServiceCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            DeleteFileFromFileServiceCommand command = new(request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("files")]
        public async Task<EndpointResult<Guid>> GetUploadFullFileUrl(
            [FromBody] GetPresignedUrlToUploadFullFileToFileServiceRequest request,
            [FromServices] ICommandHandler<
                GetPresignedUrlToUploadFullFileToFileServiceCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadFullFileToFileServiceCommand command = new(request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpGet("files")]
        public async Task<EndpointResult<Guid>> GetDownloadFileUrl(
            [FromBody] GetPresignedUrlToDownloadFullFileFromFileServiceRequest request,
            [FromServices] ICommandHandler<
                GetPresignedUrlToDownloadFullFileFromFileServiceCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToDownloadFullFileFromFileServiceCommand command = new(
                request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("files/multipart")]
        public async Task<EndpointResult<Guid>> GetStartUploadMultipartFileUrl(
            [FromBody] MultipartUploadStartRequest request,
            [FromServices] ICommandHandler<MulipartUploadStartCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            MulipartUploadStartCommand command = new(request.FileName);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpDelete("files/multipart")]
        public async Task<EndpointResult<Guid>> CancelMultipartUpload(
            [FromBody] MultipartUploadCancelRequest request,
            [FromServices] ICommandHandler<MulipartUploadCancelCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            MulipartUploadCancelCommand command = new(
                request.FileName, request.UploadId);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPut("files/multipart")]
        public async Task<EndpointResult<Guid>> CompleteMultipartUpload(
            [FromBody] MultipartUploadCompleteRequest request,
            [FromServices] ICommandHandler<MulipartUploadCompleteCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            MulipartUploadCompleteCommand command = new(
                request.FileName, request.UploadId, request.ETags);
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPost("files/miltipart")]
        public async Task<EndpointResult<Guid>> GetUploadChunkFileUrl(
            [FromBody] GetPresignedUrlToUploadChunkOfFileToFileServiceRequest request,
            [FromServices] ICommandHandler<
                GetPresignedUrlToUploadChunkOfFileToFileServiceCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            GetPresignedUrlToUploadChunkOfFileToFileServiceCommand command = new(
                request.FileName,
                request.UploadId,
                request.PartNumber);
            return await handler.HandleAsync(command, cancellationToken);
        }
    }
}
