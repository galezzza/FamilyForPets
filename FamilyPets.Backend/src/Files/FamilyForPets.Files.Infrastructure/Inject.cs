using FamilyForPets.Files.Infrastructure.Options;
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
    }
}
