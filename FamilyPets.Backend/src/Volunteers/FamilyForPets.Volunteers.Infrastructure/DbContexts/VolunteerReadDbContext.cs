using FamilyForPets.Volunteers.Contracts.DTOs;
using FamilyForPets.Volunteers.Contracts.Responses;
using FamilyForPets.Volunteers.Infrastructure.Constants;
using FamilyForPets.Volunteers.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.Infrastructure.DbContexts
{
    public class VolunteerReadDbContext(IConfiguration configuration) : DbContext, IReadDbContext
    {
        public IQueryable<VolunteerDTO> Volunteers => Set<VolunteerDTO>().AsQueryable();

        public IQueryable<PetDTO> Pets => Set<PetDTO>().AsQueryable();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(InfrastrucutreConstants.DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(VolunteerReadDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);
            base.OnModelCreating(modelBuilder);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
