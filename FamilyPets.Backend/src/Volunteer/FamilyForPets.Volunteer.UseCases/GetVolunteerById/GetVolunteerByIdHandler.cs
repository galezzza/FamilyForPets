using CSharpFunctionalExtensions;
using FamilyForPets.Shared;
using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Shared.Extentions.ValidationExtentions;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.Domain.VolunteerValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.Volunteers.UseCases.GetVolunteerById
{
    public class GetVolunteerByIdHandler : ICommandHandler<GetVolunteerByIdCommand, Volunteer>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<GetVolunteerByIdCommand> _validator;
        private readonly ILogger<GetVolunteerByIdHandler> _logger;

        public GetVolunteerByIdHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<GetVolunteerByIdCommand> validator,
            ILogger<GetVolunteerByIdHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Volunteer, ErrorList>> HandleAsync(
            GetVolunteerByIdCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
                return Result.Failure<Volunteer, ErrorList>(validationResult.ToErrorListFromValidationResult());

            VolunteerId id = VolunteerId.Create(command.Id);
            Result<Volunteer, Error> volunteerFoundedById = await _volunteerRepository.GetById(id, cancellationToken);
            if (volunteerFoundedById.IsFailure)
            {
                return Result.Failure<Volunteer, ErrorList>(
                    Errors.Volunteer.NotFound(new(nameof(VolunteerId), id)).ToErrorList());
            }

            _logger.LogInformation("Founed volunteer with id: {id}", volunteerFoundedById.Value);

            return Result.Success<Volunteer, ErrorList>(volunteerFoundedById.Value);
        }
    }
}