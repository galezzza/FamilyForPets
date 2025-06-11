using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerSoft
{
    public class SoftDeleteVolunteerHandler : ICommandHandler<SoftDeleteVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<SoftDeleteVolunteerCommand> _validator;
        private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

        public SoftDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<SoftDeleteVolunteerCommand> validator,
            ILogger<SoftDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            SoftDeleteVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return validationResult.ToErrorListFromValidationResult();

            VolunteerId volunteerId = VolunteerId.Create(command.Id);

            Result<Volunteer, Error> volunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteerResult.Error.ToErrorList());

            Volunteer volunteer = volunteerResult.Value;

            volunteer.SoftDelete();

            Result<Guid, Error> dbResult = await _volunteerRepository.Save(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            return Result.Success<Guid, ErrorList>(dbResult.Value);
        }
    }
}
