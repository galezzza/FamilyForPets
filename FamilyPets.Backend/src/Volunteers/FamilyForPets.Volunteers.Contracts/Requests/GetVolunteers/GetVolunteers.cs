namespace FamilyForPets.Volunteers.Contracts.Requests.GetVolunteers
{
    public record GetVolunteers(
        string? searchString,
        string? filtrationString,
        string? sortitionString,
        string? paginationString
        );
}
