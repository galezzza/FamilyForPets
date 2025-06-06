﻿using Microsoft.Extensions.DependencyInjection;

namespace FamilyForPets.Species.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddSpeciesInfrastrucutre(this IServiceCollection services)
        {
            services.AddScoped<SpeciesDbContext>();

            return services;
        }
    }
}
