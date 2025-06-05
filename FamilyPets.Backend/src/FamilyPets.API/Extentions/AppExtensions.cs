using System.Diagnostics;
using FamilyForPets.Species.Infrastructure;
using FamilyForPets.Volunteers.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FamilyForPets.API.Extentions
{
    public static class AppExtensions
    {
        //public static async Task ApplyMigration(this WebApplication app)
        //{
        //    await using AsyncServiceScope scope = app.Services.CreateAsyncScope();

        //    SpeciesDbContext speciesDbContext = scope.ServiceProvider
        //        .GetRequiredService<SpeciesDbContext>();
        //    VolunteerDbContext volunteerDbContext = scope.ServiceProvider
        //        .GetRequiredService<VolunteerDbContext>();

        //    await speciesDbContext.Database.MigrateAsync();

        //    await volunteerDbContext.Database.MigrateAsync();
        //}

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
