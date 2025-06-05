using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Domain.VolunteerAgregate.VolunteerValueObjects;
using FamilyForPets.Shared;
using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.Extentions.ValidationExtentions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks
{
    public class UpdateVolunteerSocialNetworksHandler : ICommandHandler<UpdateVolunteerSocialNetworksCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateVolunteerSocialNetworksCommand> _validator;
        private readonly ILogger<UpdateVolunteerSocialNetworksHandler> _logger;

        public UpdateVolunteerSocialNetworksHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateVolunteerSocialNetworksCommand> validator,
            ILogger<UpdateVolunteerSocialNetworksHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerSocialNetworksCommand command,
            CancellationToken cancellationToken)
        {
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

            Result<Guid, Error> dbResult = await _volunteerRepository.Save(volunteer, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Guid resultId = dbResult.Value;

            _logger.LogInformation("Updated social networks for volunteer with id: {id}", resultId);

            return Result.Success<Guid, ErrorList>(resultId);
        }
    }
}