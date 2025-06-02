using FamilyForPets.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FamilyForPets.API.Extentions
{
    public static class AppExtensions
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            await using AsyncServiceScope scope = app.Services.CreateAsyncScope();

            ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
