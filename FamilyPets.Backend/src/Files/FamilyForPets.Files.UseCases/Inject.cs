using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.UseCases.Test;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Volunteers.UseCases
{
    public static class Inject
    {
        public static IServiceCollection AddFilesUseCases(this IServiceCollection services)
        {
            services.AddScoped
                <ICommandHandler<TestCommand, Guid>, TestHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
