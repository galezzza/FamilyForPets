using System.Runtime.CompilerServices;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Infrastructure.Repositories;
using FamilyForPets.UseCases.VolunteerAgregate;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Infrastructure
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
