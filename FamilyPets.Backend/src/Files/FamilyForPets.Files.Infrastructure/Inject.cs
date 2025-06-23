using Amazon.S3;
using FamilyForPets.Files.Infrastructure.Options;
using FamilyForPets.Files.Infrastructure.Providers;
using FamilyForPets.Files.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace FamilyForPets.Files.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddFilesInfrastrucutre(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMinio(configuration);
            services.AddS3Client(configuration);

            services.AddScoped<IFilesProvider, S3Provider>();
            services.AddScoped<MinioOptions>(); // ???????
            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MinioOptions>(
                configuration.GetSection(MinioOptions.MINIO));

            services.AddMinio(options =>
            {
                MinioOptions minioOption = configuration.GetSection(MinioOptions.MINIO)
                    .Get<MinioOptions>() ?? throw new ApplicationException("Minio confuguration");

                options.WithEndpoint(minioOption.Endpoint);

                options.WithCredentials(minioOption.AccessKey, minioOption.SecretKey);
                options.WithSSL(minioOption.IsWithSSL);
            });

            return services;
        }

        private static IServiceCollection AddS3Client(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAmazonS3>(_ =>
            {
                MinioOptions minioOption = configuration.GetSection(MinioOptions.MINIO)
                    .Get<MinioOptions>() ?? throw new ApplicationException("Minio confuguration");

                var config = new AmazonS3Config
                {
                    ServiceURL = "http://localhost:9000",
                    ForcePathStyle = true,
                    UseHttp = !minioOption.IsWithSSL,
                };
                return new AmazonS3Client(minioOption.AccessKey, minioOption.SecretKey, config);
            });

            return services;
        }
    }
}
