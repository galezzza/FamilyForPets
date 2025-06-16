using FamilyForPets.Files.Infrastructure;
using FamilyForPets.Species.Infrastructure;
using FamilyForPets.Volunteers.Infrastructure;
using FamilyForPets.Volunteers.UseCases;

namespace FamilyForPets.WEB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVolunteerModule(
            this IServiceCollection services)
        {
            services.AddVolunteersInfrastrucutre();
            services.AddVolunteersUseCases();

            return services;
        }

        public static IServiceCollection AddSpeciesModule(
            this IServiceCollection services)
        {
            services.AddSpeciesInfrastrucutre();

            return services;
        }

        public static IServiceCollection AddFilesModule(
            this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddFilesInfrastrucutre(configuration);
            services.AddFilesUseCases();

            return services;
        }
    }
}
