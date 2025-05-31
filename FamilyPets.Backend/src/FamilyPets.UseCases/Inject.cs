using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.UseCases
{
    public static class Inject
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped
                <ICommandHandler<CreateVolunteerCommand, Guid>, CreateVolunteerHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
