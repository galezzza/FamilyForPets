﻿using System.Data.Common;
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

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer
{
    public class UpdateVolunteerHandler : ICommandHandler<UpdateVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateVolunteerCommand> _validator;
        private readonly ILogger<UpdateVolunteerHandler> _logger;

        public UpdateVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateVolunteerCommand> validator,
            ILogger<UpdateVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            // create properties of Volunteer from command
            VolunteerId id = VolunteerId.Create(command.Id);
            IEnumerable<SocialNetwork> socialNetworks = command.SocialNetworks
                .Select(sn => SocialNetwork.Create(sn.Name, sn.Url).Value);
            VolunteerSocialNetworksList socialNetworksList = VolunteerSocialNetworksList.Create(socialNetworks).Value;
            PhoneNumber phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
            EmailAdress email = EmailAdress.Create(command.EmailAdress).Value;
            FullName fullName = FullName.Create(
                command.FullName.Name, command.FullName.Surname, command.FullName.AdditionalName).Value;
            VolunteerDescription volunteerDescription = VolunteerDescription.Create(command.Description).Value;
            DetailsForPayment detailsForPayment = DetailsForPayment.Create(
                            command.PaymentDetails.CardNumber, command.PaymentDetails.OtherPaymentDetails).Value;

            await using DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            // validate buisness logic
            Result<Volunteer, Error> volunteerFoundedByEmail = await _volunteerRepository
                .GetByEmail(email, cancellationToken);
            if (volunteerFoundedByEmail.IsSuccess)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.ConflictAlreadyExists(nameof(EmailAdress)).ToErrorList());
            }

            // get volunteer to change
            Result<Volunteer, Error> volunteerFoundedById = await _volunteerRepository.GetById(id, cancellationToken);
            if (volunteerFoundedById.IsFailure)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.NotFound(new(nameof(VolunteerId), id)).ToErrorList());
            }

            Volunteer volunteer = volunteerFoundedById.Value;

            // do operations with volunteer
            UnitResult<Error> resultContactData = volunteer.UpdateContactData(phoneNumber, email);
            if (resultContactData.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            UnitResult<Error> resultPaymentDetails = volunteer.UpdateDetailsForPayment(detailsForPayment);
            if (resultPaymentDetails.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            UnitResult<Error> resultmMainInfo = volunteer.UpdateMainInfo(fullName, volunteerDescription);
            if (resultmMainInfo.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            UnitResult<Error> resultSocialNetworks = volunteer.UpdateSocialNetworks(socialNetworksList);
            if (resultSocialNetworks.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            try
            {
                // save changed to database
                await _unitOfWork.SaveChanges(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                // return success operation and log it
                Guid resultId = volunteer.Id.Value;

                _logger.LogInformation("Updating operation for volunteer with id: {id} succeeded", resultId);

                return Result.Success<Guid, ErrorList>(resultId);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogInformation("Updating operation for volunteer with id: {id} failed. Transaction conflict", command.Id);
                _logger.LogInformation(ex.Message);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Update Volunteer").ToErrorList());
            }
        }

    }
}
