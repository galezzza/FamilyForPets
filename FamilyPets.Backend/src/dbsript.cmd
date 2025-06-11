
dotnet ef database drop -f -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef database drop -f -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\

dotnet ef migrations remove -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef migrations remove -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\

dotnet ef migrations add Initial -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef migrations add Initial -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\

dotnet ef database update -p .\Volunteers\FamilyForPets.Volunteers.Infrastructure\ -s .\FamilyPets.WEB\
dotnet ef database update -p .\Species\FamilyForPets.Species.Infrastructure\ -s .\FamilyPets.WEB\

pause