using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.DeleteVolunteer.DeleteVolunteerHard
{
    public class HardDeleteVolunteerHandler : ICommandHandler<HardDeleteVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<HardDeleteVolunteerCommand> _validator;
        private readonly ILogger<HardDeleteVolunteerHandler> _logger;

        public HardDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<HardDeleteVolunteerCommand> validator,
            ILogger<HardDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            HardDeleteVolunteerCommand command,
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

            Result<Guid, Error> dbResult = await _volunteerRepository.Delete(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            return Result.Success<Guid, ErrorList>(dbResult.Value);
        }
    }
}
