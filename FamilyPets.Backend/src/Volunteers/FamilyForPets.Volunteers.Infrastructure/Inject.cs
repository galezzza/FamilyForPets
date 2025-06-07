using FamilyForPets.Volunteers.Infrastructure.Repositories;
using FamilyForPets.Volunteers.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Volunteers.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddVolunteerInfrastrucutre(this IServiceCollection services)
        {
            services.AddScoped<VolunteerDbContext>();

            services.AddScoped<IVolunteerRepository, VolunteersRepository>();

            return services;
        }
    }
}
