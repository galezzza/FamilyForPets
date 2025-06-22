using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Contracts.Requests.Download;
using FamilyForPets.Files.UseCases.Delete;
using FamilyForPets.Files.UseCases.Download;
using FamilyForPets.Files.UseCases.Upload.Fullfile;
using FamilyForPets.Files.UseCases.Upload.Multipart.Cancel;
using FamilyForPets.Files.UseCases.Upload.Multipart.Complete;
using FamilyForPets.Files.UseCases.Upload.Multipart.Start;
using FamilyForPets.Files.UseCases.Upload.Multipart.UploadChunk;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Files.UseCases
{
    public static class Inject
    {
        public static IServiceCollection AddFilesUseCases(this IServiceCollection services)
        {
            services.AddScoped
                <ICommandHandler<DeleteFileFromFileServiceCommand, Guid>,
                    DeleteFileFromFileServiceHandler>();

            services.AddScoped
                <ICommandHandler<GetPresignedUrlToUploadFullFileToFileServiceCommand, string>,
                    GetPresignedUrlToUploadFullFileToFileServiceHandler>();

            services.AddScoped
                <ICommandHandler<GetPresignedUrlToDownloadFullFileFromFileServiceCommand, string>,
                    GetPresignedUrlToDownloadFullFileFromFileServiceHandler>();

            services.AddScoped
                <ICommandHandler<MultipartUploadStartCommand, MultipartUploadStartCommandResponse>,
                    MultipartUploadStartHandler>();

            services.AddScoped
                <ICommandHandler<MultipartUploadCancelCommand>,
                    MultipartUploadCancelHandler>();

            services.AddScoped
                <ICommandHandler<MultipartUploadCompleteCommand, string>,
                    MultipartUploadCompleteHandler>();

            services.AddScoped
                <ICommandHandler<GetPresignedUrlToUploadChunkOfFileToFileServiceCommand,
                GetPresignedUrlToUploadChunkOfFileToFileServiceCommandResponse>,
                    GetPresignedUrlToUploadChunkOfFileToFileServiceHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
