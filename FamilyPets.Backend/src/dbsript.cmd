
dotnet ef database drop -f -c SpeciesDbContext -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef migrations remove -c SpeciesDbContext -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef migrations add Initial -c SpeciesDbContext -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef database update -c SpeciesDbContext -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\

dotnet ef database drop -f -c VolunteerWriteDbContext -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef migrations remove -c VolunteerWriteDbContext -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef migrations add Initial -c VolunteerWriteDbContext -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef database update -c VolunteerWriteDbContext -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\


pause