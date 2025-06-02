using FamilyForPets.API.Extentions;
using FamilyForPets.Infrastructure;
using FamilyForPets.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastrucutre()
    .AddUseCases();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
