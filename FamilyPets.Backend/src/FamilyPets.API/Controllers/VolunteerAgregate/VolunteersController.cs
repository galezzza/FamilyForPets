using FamilyForPets.API.Controllers.VolunteerAgregate.Requests.CreateVolunteer;
using FamilyForPets.API.Responses.EndpointResults;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;
using Microsoft.AspNetCore.Mvc;

namespace FamilyForPets.API.Controllers.VolunteerAgregate
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
    }
}
