namespace FamilyForPets.UseCases.DTOs
{
    public record FullNameDto(
        string Name,
        string? Surname,
        string? AdditionalName);
}
