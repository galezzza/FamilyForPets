using FamilyForPets.Domain.VolunteerAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Infrastructure
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        private const string DATABASE = "Database";

        public DbSet<Pet> Pets => Set<Pet>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
