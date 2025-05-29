using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
