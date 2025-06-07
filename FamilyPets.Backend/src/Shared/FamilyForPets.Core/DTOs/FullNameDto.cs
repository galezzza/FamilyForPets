namespace FamilyForPets.Core.DTOs
{
    public record FullNameDto(
        string Name,
        string? Surname,
        string? AdditionalName);
}
