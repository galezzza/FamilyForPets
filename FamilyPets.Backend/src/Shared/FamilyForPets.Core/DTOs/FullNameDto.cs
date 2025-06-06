namespace FamilyForPets.Shared.DTOs
{
    public record FullNameDto(
        string Name,
        string? Surname,
        string? AdditionalName);
}
