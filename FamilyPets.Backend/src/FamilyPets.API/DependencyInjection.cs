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
    }
}
