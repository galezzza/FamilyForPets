using FamilyForPets.Core.Abstractions;
using FamilyForPets.Files.Shared.DTOs;

namespace FamilyForPets.Files.UseCases.Delete
{
    public record DeleteFileFromFileServiceCommand(
        FileName FileName) : ICommand;
}
