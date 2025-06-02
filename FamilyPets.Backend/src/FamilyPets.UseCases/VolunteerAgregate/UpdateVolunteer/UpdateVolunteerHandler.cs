using CSharpFunctionalExtensions;
using FamilyForPets.Domain.VolunteerAgregate;
using FamilyForPets.Shared;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.VolunteerAgregate.GetVolunteerById;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerContactData;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerMainInfo;
using FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer.UpdateVolunteerSocialNetworks;
using Microsoft.Extensions.Logging;

namespace FamilyForPets.UseCases.VolunteerAgregate.UpdateVolunteer
{
    public class UpdateVolunteerHandler : ICommandHandler<UpdateVolunteerCommand, Guid>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<UpdateVolunteerHandler> _logger;
        private readonly ILogger<GetVolunteerByIdHandler> _logger1;
        private readonly ILogger<UpdateVolunteerSocialNetworksHandler> _logger2;
        private readonly ILogger<UpdateVolunteerDetailsForPaymentHandler> _logger3;
        private readonly ILogger<UpdateVolunteerMainInfoHandler> _logger4;
        private readonly ILogger<UpdateVolunteerContactDataHandler> _logger5;

        public UpdateVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            ILogger<UpdateVolunteerHandler> logger,
            ILogger<GetVolunteerByIdHandler> logger1,
            ILogger<UpdateVolunteerSocialNetworksHandler> logger2,
            ILogger<UpdateVolunteerDetailsForPaymentHandler> logger3,
            ILogger<UpdateVolunteerMainInfoHandler> logger4,
            ILogger<UpdateVolunteerContactDataHandler> logger5)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _logger1 = logger1;
            _logger2 = logger2;
            _logger3 = logger3;
            _logger4 = logger4;
            _logger5 = logger5;
        }

        public async Task<Result<Guid, ErrorList>> HandleAsync(
            UpdateVolunteerCommand command,
            CancellationToken cancellationToken)
        {
            // split command into specified command
            GetVolunteerByIdCommand command1 = new (
                command.Id);
            UpdateVolunteerSocialNetworksCommand command2 = new (
                command.Id, command.SocialNetworks);
            UpdateVolunteerDetailsForPaymentCommand command3 = new(
                command.Id, new(command.CardNumber, command.OtherDetails));
            UpdateVolunteerMainInfoCommand command4 = new(
                command.Id, command.FullName, command.Description);
            UpdateVolunteerContactDataCommand command5 = new(
                command.Id, command.EmailAdress, command.PhoneNumber);

            // initialize specified handlers
            GetVolunteerByIdHandler handler1 = new (
                _volunteerRepository, new GetVolunteerByIdCommandValidator(), _logger1);
            UpdateVolunteerSocialNetworksHandler handler2 = new(
                _volunteerRepository, new UpdateVolunteerSocialNetworksCommandValidator(), _logger2);
            UpdateVolunteerDetailsForPaymentHandler handler3 = new(
                _volunteerRepository, new UpdateVolunteerDetailsForPaymentCommandValidator(), _logger3);
            UpdateVolunteerMainInfoHandler handler4 = new(
                _volunteerRepository, new UpdateVolunteerMainInfoCommandValidator(), _logger4);
            UpdateVolunteerContactDataHandler handler5 = new(
                _volunteerRepository, new UpdateVolunteerContactDataCommandValidator(), _logger5);

            // update volunteer entity one by one
            Result<Volunteer, ErrorList> volunteer = await handler1.HandleAsync(command1, cancellationToken);
            if (volunteer.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteer.Error);

            volunteer = await handler2.HandleAsyncWithoutSavingToDb(volunteer.Value, command2, cancellationToken);
            if (volunteer.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteer.Error);

            volunteer = await handler3.HandleAsyncWithoutSavingToDb(volunteer.Value, command3, cancellationToken);
            if (volunteer.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteer.Error);

            volunteer = await handler4.HandleAsyncWithoutSavingToDb(volunteer.Value, command4, cancellationToken);
            if (volunteer.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteer.Error);

            volunteer = await handler5.HandleAsyncWithoutSavingToDb(volunteer.Value, command5, cancellationToken);
            if (volunteer.IsFailure)
                return Result.Failure<Guid, ErrorList>(volunteer.Error);

            // save changed to database
            Result<Guid, Error> dbResult = await _volunteerRepository.Save(volunteer.Value, cancellationToken);
            if (dbResult.IsFailure)
                return Result.Failure<Guid, ErrorList>(Errors.General.Failure().ToErrorList());

            Guid resultId = dbResult.Value;

            _logger.LogInformation("Updating operation for volunteer with id: {id} succeeded", resultId);

            return Result.Success<Guid, ErrorList>(resultId);
        }

    }
}
