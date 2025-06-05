using FamilyForPets.Species.Infrastructure;
using FamilyForPets.Volunteers.Infrastructure;
using FamilyForPets.Volunteers.UseCases;

namespace FamilyForPets.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVolunteerModule(
            this IServiceCollection services)
        {
            services.AddVolunteerInfrastrucutre();
            services.AddVolunteerUseCases();

            return services;
        }

        public static IServiceCollection AddSpeciesModule(
            this IServiceCollection services)
        {
            services.AddSpeciesInfrastrucutre();

            return services;
        }
    }
}
