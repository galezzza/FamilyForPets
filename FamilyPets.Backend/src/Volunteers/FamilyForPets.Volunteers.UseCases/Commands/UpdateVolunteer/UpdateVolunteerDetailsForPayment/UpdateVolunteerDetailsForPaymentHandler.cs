using System.Data.Common;
using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Database;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerDetailsForPayment
{
    public class UpdateVolunteerDetailsForPaymentHandler : ICommandHandler<UpdateVolunteerDetailsForPaymentCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateVolunteerDetailsForPaymentCommand> _validator;
        private readonly ILogger<UpdateVolunteerDetailsForPaymentHandler> _logger;

        public UpdateVolunteerDetailsForPaymentHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateVolunteerDetailsForPaymentCommand> validator,
            ILogger<UpdateVolunteerDetailsForPaymentHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
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

            await using DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);

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

            try
            {
                // save changed to database
                await _unitOfWork.SaveChanges(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                // return success operation and log it
                Guid resultId = volunteer.Id.Value;

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("Updated payment details for volunteer with id: {id} succeeded", resultId);

                return Result.Success<Guid, ErrorList>(resultId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogInformation("Updated payment details for volunteer with id: {id} failed. Transaction conflict", command.Id);
                _logger.LogInformation(ex.Message);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Update Volunteer payment details").ToErrorList());
            }
        }
    }
}
