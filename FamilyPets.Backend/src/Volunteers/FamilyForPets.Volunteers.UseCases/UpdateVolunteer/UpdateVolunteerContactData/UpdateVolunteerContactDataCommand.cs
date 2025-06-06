﻿using FamilyForPets.Core.Abstractions;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerContactData
{
    public record UpdateVolunteerContactDataCommand(
        Guid Id,
        string Email,
        string PhoneNumber) : ICommand;
}
