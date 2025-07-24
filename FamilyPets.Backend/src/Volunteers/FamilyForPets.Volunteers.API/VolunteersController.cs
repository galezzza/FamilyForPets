using FamilyForPets.Core.Abstractions;
using FamilyForPets.Core.DTOs;
using FamilyForPets.Framework.Responses.EndpointResults;
using FamilyForPets.Volunteers.Contracts;
using FamilyForPets.Volunteers.Contracts.Requests.CreateVolunteer;
using FamilyForPets.Volunteers.Contracts.Requests.UpdateVolunteer;
using FamilyForPets.Volunteers.Contracts.Responses;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.UseCases;
using FamilyForPets.Volunteers.UseCases.Commands.CreateVolunteer;
using FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerHard;
using FamilyForPets.Volunteers.UseCases.Commands.DeleteVolunteer.DeleteVolunteerSoft;
using FamilyForPets.Volunteers.UseCases.Commands.GetVolunteerById;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerContactData;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerMainInfo;
using FamilyForPets.Volunteers.UseCases.Commands.UpdateVolunteer.UpdateVolunteerSocialNetworks;
using FamilyForPets.Volunteers.UseCases.Queries.GetVolunteers;
using Microsoft.AspNetCore.Mvc;

namespace FamilyForPets.Volunteers.API
{
    // default controller logic
    //
    // 1) validate inputs
    //      1.1) because of Request and Command are the same,
    //          we do not validate inputs as request in controller
    //          we will validate inputs as command only on next layer
    //
    // 2) create Command from Request (they are the same)
    //
    // 4) send to UseCases layer to handle logic
    //      if result is not valid return envelope with errors
    //
    // 5) if ok return envelope with response
    //      5.1) EndpointResult implicits Result<> to Response
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpPost]
        public async Task<EndpointResult<Guid>> Create(
            [FromBody] CreateVolunteerRequest request,
            [FromServices] ICommandHandler<CreateVolunteerCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            CreateVolunteerCommand command = new CreateVolunteerCommand(
                new FullNameDto(
                    request.Name,
                    request.Surname,
                    request.AdditionalName),
                request.Email,
                request.ExperienceInYears,
                request.PhoneNumber,
                new PaymentDetailsDto(
                    request.CardNumber,
                    request.OtherPaymentDetails));

            // command = request.ToCommand();

            // implicit from Result<T, E> to ResponseEnvelope
            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpGet("{id:guid}")]
        public async Task<EndpointResult<Volunteer>> GetById(
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<GetVolunteerByIdCommand, Volunteer> handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.HandleAsync(new GetVolunteerByIdCommand(id), cancellationToken);
        }

        [HttpPatch("{id:guid}/social-networks")]
        public async Task<EndpointResult<Guid>> UpdateSocialNewtworks(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerSocialNetworksRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerSocialNetworksCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            UpdateVolunteerSocialNetworksCommand command = new UpdateVolunteerSocialNetworksCommand(
                id, request.SocialNetworks);

            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPatch("{id:guid}/details-for-payment")]
        public async Task<EndpointResult<Guid>> UpdateDetailsForPayment(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerDetailsForPaymentRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerDetailsForPaymentCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            UpdateVolunteerDetailsForPaymentCommand command = new UpdateVolunteerDetailsForPaymentCommand(
                id, new(request.CardNumber, request.OtherDetails));

            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPatch("{id:guid}/contact-data")]
        public async Task<EndpointResult<Guid>> UpdateContactData(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerContactDataRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerContactDataCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            UpdateVolunteerContactDataCommand command = new UpdateVolunteerContactDataCommand(
                id, request.Email, request.PhoneNumber);

            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPatch("{id:guid}/main-info")]
        public async Task<EndpointResult<Guid>> UpdateMainInfoData(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerMainInfoRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerMainInfoCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            UpdateVolunteerMainInfoCommand command = new UpdateVolunteerMainInfoCommand(
                id, new(request.Name, request.Surname, request.AdditionalName), request.Description);

            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPut("{id:guid}")]
        public async Task<EndpointResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            UpdateVolunteerCommand command = new UpdateVolunteerCommand(
                id,
                request.SocialNetworks,
                new PaymentDetailsDto(request.CardNumber, request.OtherDetails),
                new FullNameDto(
                    request.Name, request.Surname, request.AdditionalName),
                request.Description,
                request.PhoneNumber, request.Email);

            return await handler.HandleAsync(command, cancellationToken);
        }

        [HttpPatch("{id:guid}/deletion-status/soft")]
        public async Task<EndpointResult<Guid>> DeleteSoft(
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<SoftDeleteVolunteerCommand, Guid> softDeleteHandler,
            CancellationToken cancellationToken)
        {
            return await softDeleteHandler.HandleAsync(new(id), cancellationToken);
        }

        [HttpDelete("{id:guid}/deletion-status/hard")]
        public async Task<EndpointResult<Guid>> DeleteHard(
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<HardDeleteVolunteerCommand, Guid> hardDeleteHandler,
            CancellationToken cancellationToken)
        {
            return await hardDeleteHandler.HandleAsync(new(id), cancellationToken);
        }

        [HttpGet]
        public async Task<EndpointResult<IReadOnlyList<VolunteerDTO>>> GetVolunteers(
            [FromServices] IQueryHandler<
                GetVolunteersQuery,
                IReadOnlyList<VolunteerDTO>> getVolunteersHandler,
            CancellationToken cancellationToken)
        {
            return await getVolunteersHandler.HandleAsync(new(), cancellationToken);
        }
    }
}
