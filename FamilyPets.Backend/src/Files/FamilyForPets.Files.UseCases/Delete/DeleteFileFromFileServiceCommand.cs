using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;

namespace FamilyForPets.Files.UseCases.Delete
{
    public record DeleteFileFromFileServiceCommand(
        FileName FileName) : ICommand;
}
