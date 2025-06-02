using System.Diagnostics;
using FamilyForPets.API.Extentions;
using FamilyForPets.API.Middlewares;
using FamilyForPets.Infrastructure;
using FamilyForPets.UseCases;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") ?? throw new ArgumentException("Seq"))
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSerilog();
builder.Services.AddSingleton<ExceptionMiddleware>();

builder.Services
    .AddInfrastrucutre()
    .AddUseCases();

var app = builder.Build();

app.UseExceptionMiddleware();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigration();
    await app.OpenSeqInBrowser();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
