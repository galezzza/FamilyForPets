using FamilyForPets.Shared;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FamilyForPets.API.Extentions
{
    public static class ResponseExtentions
    {
        public static ActionResult ToResponse(this Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            var responseError = new ErrorForEnvelope(error.Code, error.Message, null);

            ResponseEnvelope envelope = ResponseEnvelope.Error([responseError]);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        public static ActionResult ToValidationErrorResponse(this ValidationResult result)
        {
            if (result.IsValid)
                throw new InvalidOperationException("Result cannot be succeed");

            List<ValidationFailure> validationErrors = result.Errors;

            // make from validation errors (FluentValidation) to "Error" errors
            var errors = from validationError in validationErrors
                         let serializedErrorAsMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(serializedErrorAsMessage)
                         let resposeError = new ErrorForEnvelope(error.Code, error.Message, validationError.PropertyName)
                         select resposeError;

            // errors in envelope
            ResponseEnvelope envelope = ResponseEnvelope.Error(errors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }
    }
}
