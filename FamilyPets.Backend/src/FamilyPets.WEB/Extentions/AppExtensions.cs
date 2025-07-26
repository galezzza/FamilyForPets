using System.Diagnostics;
using FamilyForPets.Species.Infrastructure;
using FamilyForPets.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FamilyForPets.WEB.Extentions
{
    public static class AppExtensions
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            await using AsyncServiceScope scope2 = app.Services.CreateAsyncScope();

            VolunteerWriteDbContext volunteerDbContext = scope2.ServiceProvider
                .GetRequiredService<VolunteerWriteDbContext>();
            await volunteerDbContext.Database.MigrateAsync();

            await using AsyncServiceScope scope1 = app.Services.CreateAsyncScope();

            SpeciesDbContext speciesDbContext = scope1.ServiceProvider
                .GetRequiredService<SpeciesDbContext>();
            await speciesDbContext.Database.MigrateAsync();
        }

        public static void OpenSeqInBrowser(this WebApplication app)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = app.Configuration.GetConnectionString("SeqUI") ?? throw new ArgumentException("SeqUI"),
                UseShellExecute = true,
            });
        }
    }
}
