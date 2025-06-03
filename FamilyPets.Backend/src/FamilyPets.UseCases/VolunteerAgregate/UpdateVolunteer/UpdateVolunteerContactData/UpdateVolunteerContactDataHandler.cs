using System.Net.Mail;
using CSharpFunctionalExtensions;
using FamilyForPets.Domain.SharedValueObjects;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.Extentions.ValidationExtentions;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerMainInfo;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerContactData
{
    public class UpdateVolunteerContactDataHandler : ICommandHandler<UpdateVolunteerContactDataCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateVolunteerContactDataCommand> _validator;
        private readonly ILogger<UpdateVolunteerContactDataHandler> _logger;

        public UpdateVolunteerContactDataHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateVolunteerContactDataCommand> validator,
            ILogger<UpdateVolunteerContactDataHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerContactDataCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            VolunteerId id = VolunteerId.Create(command.Id);
            PhoneNumber phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            EmailAdress email = EmailAdress.Create(command.Email).Value;

            // validate buisness logic
            Result<Volunteer, Error> volunteerFoundedByEmail = await _volunteerRepository
                .GetByEmail(email, cancellationToken);
            if (volunteerFoundedByEmail.IsSuccess)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.ConflictAlreadyExists(nameof(EmailAdress)).ToErrorList());
            }

            Result<Volunteer, Error> volunteerFoundedById = await _volunteerRepository
                .GetById(id, cancellationToken);
            if (volunteerFoundedById.IsFailure)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.NotFound(new(nameof(VolunteerId), id)).ToErrorList());
            }

            Volunteer volunteer = volunteerFoundedById.Value;

            UnitResult<Error> result = volunteer.UpdateContactData(phoneNumber, email);
            if (result.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Result<Guid, Error> dbResult = await _volunteerRepository.Save(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Guid resultId = dbResult.Value;

            _logger.LogInformation("Updated contact data for volunteer with id: {id}", resultId);

            return Result.Success<Guid, ErrorList>(resultId);
        }
    }
}
