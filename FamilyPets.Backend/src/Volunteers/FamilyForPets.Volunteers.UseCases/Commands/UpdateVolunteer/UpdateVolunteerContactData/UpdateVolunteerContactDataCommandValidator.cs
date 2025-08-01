﻿using FamilyForPets.Core.Validation;
using FamilyForPets.SharedKernel.ValueObjects;
using FluentValidation;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerContactData
{
    public class UpdateVolunteerContactDataCommandValidator : AbstractValidator<UpdateVolunteerContactDataCommand>
    {
        public UpdateVolunteerContactDataCommandValidator()
        {
            RuleFor(cvc => cvc.Email)
                .MustBeValueObject(EmailAdress.Create);

            RuleFor(cvc => cvc.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);
        }
    }
}
