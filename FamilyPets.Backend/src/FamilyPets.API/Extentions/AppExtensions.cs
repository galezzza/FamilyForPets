using System.Diagnostics;
using System.Net.Http.Headers;
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

        public static async Task OpenSeqInBrowser(this WebApplication app)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = app.Configuration.GetConnectionString("SeqUI") ?? throw new ArgumentException("SeqUI"),
                UseShellExecute = true,
            });
        }
    }
}
