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

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerHard
{
    public class HardDeleteVolunteerHandler : ICommandHandler<HardDeleteVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<HardDeleteVolunteerCommand> _validator;
        private readonly ILogger<HardDeleteVolunteerHandler> _logger;

        public HardDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<HardDeleteVolunteerCommand> validator,
            ILogger<HardDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
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

            using DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);
            try
            {
                Result<Guid, Error> dbResult = await _volunteerRepository.Delete(volunteer, cancellationToken);
                if (dbResult.IsFailure)
                    return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

                await transaction.CommitAsync(cancellationToken);

                return Result.Success<Guid, ErrorList>(dbResult.Value);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogInformation("Hard Deletion operation for volunteer with id: {id} failed. Transaction conflict", command.Id);
                _logger.LogInformation(ex.Message);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Hard Delete Volunteer").ToErrorList());
            }

        }
    }
}
