using CSharpFunctionalExtensions;
using FamilyForPets.Domain.SharedValueObjects;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.Extentions.ValidationExtentions;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerMainInfo
{
    public class UpdateVolunteerMainInfoHandler : ICommandHandler<UpdateVolunteerMainInfoCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateVolunteerMainInfoCommand> _validator;
        private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger;

        public UpdateVolunteerMainInfoHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateVolunteerMainInfoCommand> validator,
            ILogger<UpdateVolunteerMainInfoHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerMainInfoCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            VolunteerId id = VolunteerId.Create(command.Id);
            FullName fullName = FullName.Create(
                command.FullName.Name, command.FullName.Surname, command.FullName.AdditionalName).Value;
            VolunteerDescription volunteerDescription = VolunteerDescription.Create(command.Description).Value;

            Result<Volunteer, Error> volunteerFoundedById = await _volunteerRepository.GetById(id, cancellationToken);
            if (volunteerFoundedById.IsFailure)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.NotFound(new(nameof(VolunteerId), id)).ToErrorList());
            }

            Volunteer volunteer = volunteerFoundedById.Value;

            UnitResult<Error> result = volunteer.UpdateMainInfo(fullName, volunteerDescription);
            if (result.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Result<Guid, Error> dbResult = await _volunteerRepository.Save(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Guid resultId = dbResult.Value;

            _logger.LogInformation("Updated main info for volunteer with id: {id}", resultId);

            return Result.Success<Guid, ErrorList>(resultId);
        }

        public async Task<Result<Volunteer, ErrorList>> HandleAsyncWithoutSavingToDb(
            Volunteer volunteer,
            UpdateVolunteerMainInfoCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Volunteer, ErrorList>(validationResult.ToErrorListFromValidationResult());

            FullName fullName = FullName.Create(
                command.FullName.Name, command.FullName.Surname, command.FullName.AdditionalName).Value;
            VolunteerDescription volunteerDescription = VolunteerDescription.Create(command.Description).Value;

            UnitResult<Error> result = volunteer.UpdateMainInfo(fullName, volunteerDescription);
            if (result.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            _logger.LogInformation("Updated main info for volunteer with id: {id}", volunteer.Id.Value);

            return Result.Success<Volunteer, ErrorList>(volunteer);
        }
    }
}
