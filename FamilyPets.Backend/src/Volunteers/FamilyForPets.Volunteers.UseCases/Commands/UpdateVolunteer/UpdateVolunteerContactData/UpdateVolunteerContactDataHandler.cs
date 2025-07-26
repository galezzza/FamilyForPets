using System.Data.Common;
using CSharpFunctionalExtensions;
using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.Database;
using FamilyForPets.Core.Extentions.ValidationExtentions;
using FamilyForPets.SharedKernel;
using FamilyForPets.SharedKernel.ValueObjects;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerContactData
{
    public class UpdateVolunteerContactDataHandler : ICommandHandler<UpdateVolunteerContactDataCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateVolunteerContactDataCommand> _validator;
        private readonly ILogger<UpdateVolunteerContactDataHandler> _logger;

        public UpdateVolunteerContactDataHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateVolunteerContactDataCommand> validator,
            ILogger<UpdateVolunteerContactDataHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
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

            using DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);
            try
            {
                // save changed to database
                await _unitOfWork.SaveChanges(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                // return success operation and log it
                Guid resultId = volunteer.Id.Value;

                _logger.LogInformation("Updated contact data for volunteer with id: {id}", resultId);

                return Result.Success<Guid, ErrorList>(resultId);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogInformation("Updated contact data for volunteer with id: {id} failed. Transaction conflict", command.Id);
                _logger.LogInformation(ex.Message);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Update Volunteer contact data").ToErrorList());
            }
        }
    }
}
