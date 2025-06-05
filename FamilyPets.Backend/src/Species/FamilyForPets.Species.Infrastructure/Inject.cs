using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Volunteers.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastrucutre(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            return services;
        }
    }
}
