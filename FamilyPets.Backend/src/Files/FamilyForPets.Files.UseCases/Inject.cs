using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Files.UseCases
{
    public static class Inject
    {
        public static IServiceCollection AddFilesUseCases(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
