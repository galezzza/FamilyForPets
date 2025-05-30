using CSharpFunctionalExtensions;
using FamilyForPets.API.Controllers.VolunteerAgregate.Requests.CreateVolunteer;
using FamilyForPets.API.ResponsesCommonLogic;
using FamilyForPets.Shared;
using FamilyForPets.UseCases.Abstractions;
using FamilyForPets.UseCases.VolunteerAgregate.CreateVolunteer;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FamilyForPets.API.Controllers.VolunteerAgregate
{
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
            // validate Request

            // because of Request and Command are the same,
            // we do not validate request in controller
            // we will validate inputs as command only on next layer

            // create Command from Request (they are the same)
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

            // validate command
            ValidationResult validationResult = await validator.ValidateAsync(command);
            if (validationResult.IsValid == false)
            {
                ActionResult responseFromFluentValidationErrors = validationResult.ToValidationErrorResponse();

                // return our custom error(s) as envelope || aka as response
                return responseFromFluentValidationErrors;
            }

            // send to UseCases layer to handle logic
            Result<Guid, Error> result = await handler.HandleAsync(command, cancellationToken);
            if (result.IsFailure)
            {
                ActionResult responseFromError = result.Error.ToResponse();

                // return our custom error(s) as envelope || aka as response
                return responseFromError;
            }

            // if ok return response
            return Ok(result.Value);
        }
    }
}
