using FamilyForPets.Core.Abstractions;
using FamilyForPets.SharedKernel.ValueObjects;

namespace FamilyForPets.Files.UseCases.Delete
{
    public record DeleteFileFromFileServiceCommand(
        FileName FileName) : ICommand;
}
