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
            DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);

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

            try
            { 
                // database operations
                Result<Guid, Error> dbResult = await _volunteerRepository.Add(volunteer, cancellationToken);
                if (dbResult.IsFailure)
                    return Result.Failure<Guid, ErrorList>(dbResult.Error.ToErrorList());

                _logger.LogInformation("Created volunteer with id: {id}", volunteer.Id.Value);

                // return
                return Result.Success<Guid, ErrorList>(dbResult.Value);
            }
            catch
            {
                transaction.Rollback();

                _logger.LogInformation("Creating operation for volunteer with email: {email} failed. Transaction conflict", command.Email);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Creating Volunteer").ToErrorList());
            }
        }
    }
}
