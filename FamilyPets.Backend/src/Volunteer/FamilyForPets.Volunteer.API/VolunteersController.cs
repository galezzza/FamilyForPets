using FamilyForPets.API.Responses.EndpointResults;
using FamilyForPets.Shared.Abstractions;
using FamilyForPets.Volunteers.API.Requests.CreateVolunteer;
using FamilyForPets.Volunteers.API.Requests.UpdateVolunteer;
using FamilyForPets.Volunteers.Domain.Entities;
using FamilyForPets.Volunteers.UseCases.CreateVolunteer;
using FamilyForPets.Volunteers.UseCases.GetVolunteerById;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerContactData;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerDetailsForPayment;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerMainInfo;
using FamilyForPets.Volunteers.UseCases.UpdateVolunteer.UpdateVolunteerSocialNetworks;
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
            CreateVolunteerCommand command = request.ToCommand();

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
            return await handler.HandleAsync(request.ToCommand(id), cancellationToken);
        }

        [HttpPatch("{id:guid}/details-for-payment")]
        public async Task<EndpointResult<Guid>> UpdateDetailsForPayment(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerDetailsForPaymentRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerDetailsForPaymentCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.HandleAsync(request.ToCommand(id), cancellationToken);
        }

        [HttpPatch("{id:guid}/contact-data")]
        public async Task<EndpointResult<Guid>> UpdataContactData(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerContactDataRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerContactDataCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.HandleAsync(request.ToCommand(id), cancellationToken);
        }

        [HttpPatch("{id:guid}/main-info")]
        public async Task<EndpointResult<Guid>> UpdataMainInfoData(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerMainInfoRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerMainInfoCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.HandleAsync(request.ToCommand(id), cancellationToken);
        }

        [HttpPut("{id:guid}")]
        public async Task<EndpointResult<Guid>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateVolunteerRequest request,
            [FromServices] ICommandHandler<UpdateVolunteerCommand, Guid> handler,
            CancellationToken cancellationToken = default)
        {
            return await handler.HandleAsync(request.ToCommand(id), cancellationToken);
        }
    }
}
