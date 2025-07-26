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

namespace FamilyForPets.Volunteers.UseCases.Commands.CreateVolunteer
{
    public class CreateVolunteerHandler : ICommandHandler<CreateVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        private readonly ILogger<CreateVolunteerHandler> _logger;

        public CreateVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateVolunteerCommand> validator,
            ILogger<CreateVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            CreateVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            // validate inputs
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            // inputs shoud be validated before
            FullName fullname = FullName.Create(command.FullName.Name, command.FullName.Surname, command.FullName.AdditionalName).Value;
            EmailAdress emailAdress = EmailAdress.Create(command.Email).Value;
            PhoneNumber phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            DetailsForPayment detailsForPaymentess = DetailsForPayment.Create(command.PaymentDetails.CardNumber, command.PaymentDetails.OtherPaymentDetails).Value;

            // validate buisness logic
            Result<Volunteer, Error> volunteerFoundedByEmail = await _volunteerRepository.GetByEmail(emailAdress, cancellationToken);
            if (volunteerFoundedByEmail.IsSuccess)
                return Result.Failure<Guid, ErrorList>(new ErrorList(Errors.Volunteer.ConflictAlreadyExists(nameof(EmailAdress)).ToErrorList()));

            // create domain entity
            Result<Volunteer, Error> volunteerToCreateResult = Volunteer.Create(
                fullname,
                emailAdress,
                command.ExperienceInYears,
                phoneNumber,
                detailsForPaymentess);

            if (volunteerToCreateResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteerToCreateResult.Error.ToErrorList());

            Volunteer volunteer = volunteerToCreateResult.Value;

            await using DbTransaction transaction = await _unitOfWork.BeginTransaction(
                System.Data.IsolationLevel.Serializable,
                cancellationToken);
            try
            {
                // database operations
                Result<Guid, Error> dbResult = await _volunteerRepository.Add(volunteer, cancellationToken);
                if (dbResult.IsFailure)
                    return Result.Failure<Guid, ErrorList>(dbResult.Error.ToErrorList());

                _logger.LogInformation("Created volunteer with id: {id}", volunteer.Id.Value);

                await transaction.CommitAsync(cancellationToken);

                // return
                return Result.Success<Guid, ErrorList>(dbResult.Value);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogInformation("Creating operation for volunteer with email: {email} failed. Transaction conflict", command.Email);
                _logger.LogInformation(ex.Message);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Creating Volunteer").ToErrorList());
            }
        }
    }
}
