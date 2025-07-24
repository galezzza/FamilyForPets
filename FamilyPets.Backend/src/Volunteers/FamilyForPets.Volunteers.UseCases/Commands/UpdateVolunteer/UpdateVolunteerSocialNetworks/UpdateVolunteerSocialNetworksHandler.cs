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
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public class UpdateVolunteerSocialNetworksHandler : ICommandHandler<UpdateVolunteerSocialNetworksCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateVolunteerSocialNetworksCommand> _validator;
        private readonly ILogger<UpdateVolunteerSocialNetworksHandler> _logger;

        public UpdateVolunteerSocialNetworksHandler(
            IVolunteerRepository volunteerRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateVolunteerSocialNetworksCommand> validator,
            ILogger<UpdateVolunteerSocialNetworksHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerSocialNetworksCommand command,
            CancellationToken cancellationToken)
        {
            DbTransaction transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Guid, ErrorList>(validationResult.ToErrorListFromValidationResult());

            VolunteerId id = VolunteerId.Create(command.Id);
            IEnumerable<SocialNetwork> socialNetworks = command.SocialNetworks
                .Select(sn => SocialNetwork.Create(sn.Name, sn.Url).Value);
            VolunteerSocialNetworksList socialNetworksList = VolunteerSocialNetworksList.Create(socialNetworks).Value;

            Result<Volunteer, Error> volunteerFoundedById = await _volunteerRepository.GetById(id, cancellationToken);
            if (volunteerFoundedById.IsFailure)
            {
                return Result.Failure<Guid, ErrorList>(
                    Errors.Volunteer.NotFound(new (nameof(VolunteerId), id)).ToErrorList());
            }

            Volunteer volunteer = volunteerFoundedById.Value;

            UnitResult<Error> result = volunteer.UpdateSocialNetworks(socialNetworksList);
            if (result.IsFailure)
                Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            try
            {
                // save changed to database
                await _unitOfWork.SaveChanges(cancellationToken);

                // return success operation and log it
                Guid resultId = volunteer.Id.Value;

                _logger.LogInformation("Updated social networks for volunteer with id: {id} succeeded", resultId);

                return Result.Success<Guid, ErrorList>(resultId);
            }
            catch
            {
                transaction.Rollback();

                _logger.LogInformation("Updated social networks volunteer with id: {id} failed. Transaction conflict", command.Id);

                return Result.Failure<Guid, ErrorList>(Errors.Database
                    .TransactionConflict("Update Volunteer social networks").ToErrorList());
            }
        }
    }
}