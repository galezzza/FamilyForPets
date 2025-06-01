using CSharpFunctionalExtensions;
using FamilyForPets.API.Controllers.VolunteerAgregate.Requests.CreateVolunteer;
using FamilyForPets.API.ResponsesCommonLogic;
using FamilyForPets.Shared;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.DTOs;
using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;
using FluentValidation;
using FluentValidation.Results;
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
    // 3) validate command using FluentValidation
    //      if result is not valid return envelope with errors
    //
    // 4) send to UseCases layer to handle logic
    //      if result is not valid return envelope with errors
    //
    // 5) if ok return envelope with response
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateVolunteerRequest request,
            [FromServices] ICommandHandler<CreateVolunteerCommand, Guid> handler,
            [FromServices] IValidator<CreateVolunteerCommand> validator,
            CancellationToken cancellationToken = default)
        {
            CreateVolunteerCommand command = request.ToCommand();

            Result<Guid, ErrorList> result = await handler.HandleAsync(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponseFromErrorList();

            return Ok(result.ToResponseFromSuccessResult());
        }
    }
}
