using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.DTOs;
using FluentValidation;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer
{
    public record UpdateVolunteerCommand(
        Guid Id,
        IEnumerable<SocialNetworkDTO> SocialNetworks,
        string CardNumber, string? OtherDetails,
        FullNameDto FullName, string? Description,
        string PhoneNumber, string EmailAdress) : ICommand;
}
