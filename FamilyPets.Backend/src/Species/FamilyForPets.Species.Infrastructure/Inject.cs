using System.Runtime.CompilerServices;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Volunteers.Infrastructure.Repositories;
using FamilyForPets.Volunteers.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Volunteers.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastrucutre(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IVolunteerRepository, VolunteersRepository>();

            return services;
        }
    }
}
