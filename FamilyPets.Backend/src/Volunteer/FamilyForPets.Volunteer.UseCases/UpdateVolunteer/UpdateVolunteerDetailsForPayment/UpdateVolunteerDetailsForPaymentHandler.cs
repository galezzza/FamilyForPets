using CSharpFunctionalExtensions;
using FamilyForPets.Shared;
using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.Extentions.ValidationExtentions;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public class UpdateVolunteerDetailsForPaymentHandler : ICommandHandler<UpdateVolunteerDetailsForPaymentCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateVolunteerDetailsForPaymentCommand> _validator;
        private readonly ILogger<UpdateVolunteerDetailsForPaymentHandler> _logger;

        public UpdateVolunteerDetailsForPaymentHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateVolunteerDetailsForPaymentCommand> validator,
            ILogger<UpdateVolunteerDetailsForPaymentHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerDetailsForPaymentCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            VolunteerId id = VolunteerId.Create(command.Id);
            DetailsForPayment detailsForPayment = DetailsForPayment.Create(
                command.Details.CardNumber, command.Details.OtherPaymentDetails).Value;

            Result<Volunteer, Error> volunteerFoundedById = await _volunteerRepository.GetById(id, cancellationToken);
            if (volunteerFoundedById.IsFailure)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.NotFound(new(nameof(VolunteerId), id)).ToErrorList());
            }

            Volunteer volunteer = volunteerFoundedById.Value;

            UnitResult<Error> result = volunteer.UpdateDetailsForPayment(detailsForPayment);
            if (result.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Result<Guid, Error> dbResult = await _volunteerRepository.Save(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Guid resultId = dbResult.Value;

            _logger.LogInformation("Updated payment details for volunteer with id: {id}", resultId);

            return Result.Success<Guid, ErrorList>(resultId);
        }
    }
}
