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

namespace FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerSoft
{
    public class SoftDeleteVolunteerHandler : ICommandHandler<SoftDeleteVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SoftDeleteVolunteerCommand> _validator;
        private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

        public SoftDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<SoftDeleteVolunteerCommand> validator,
            ILogger<SoftDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
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

            if (volunteer.IsDeleted == true)
                return Result.Success<Guid, ErrorList>(volunteer.Id.Value);

            DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);
            try
            {
                volunteer.SoftDelete();

                await _unitOfWork.SaveChanges(cancellationToken);

                return Result.Success<Guid, ErrorList>(volunteer.Id.Value);
            }
            catch (DbUpdateConcurrencyException ex) {
            {
                transaction.Rollback();

                _logger.LogInformation("Soft Deletion operation for volunteer with id: {id} failed. Transaction conflict", command.Id);
                _logger.LogInformation(ex.Message);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Soft Delete Volunteer").ToErrorList());
            }
        }
    }
}
