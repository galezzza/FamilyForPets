using FamilyForPets.Volunteers.Infrastructure.DbContexts;
using FamilyForPets.Volunteers.Infrastructure.Repositories;
using FamilyForPets.Volunteers.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Volunteers.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddVolunteersInfrastrucutre(this IServiceCollection services)
        {
            services.AddScoped<VolunteerWriteDbContext>();
            services.AddScoped<IReadDbContext, VolunteerReadDbContext>();

            services.AddScoped<IVolunteerRepository, VolunteersRepository>();

            return services;
        }
    }
}
