using CSharpFunctionalExtensions;
using FamilyForPets.Shared;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FamilyForPets.API.ResponsesCommonLogic
{
    public static class ResponseExtentions
    {
        public static ActionResult ToResponseFromError(this Error error)
        {
            var statusCode = GetStatusCodeForErrorType(error.Type);

            var responseError = new ErrorForEnvelope(error.Code, error.Message, null);

            ResponseEnvelope envelope = ResponseEnvelope.Error([responseError]);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        public static ActionResult ToResponseFromSuccessResult<T>(this Result<T, Error> correctResult)
        {
            if (correctResult.IsFailure)
                throw new InvalidOperationException("Result cannot be failed");

            ResponseEnvelope<T> envelope = ResponseEnvelope<T>.Correct(correctResult.Value);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public static ActionResult ToResponseFromValidationError(this ValidationResult errorResult)
        {
            if (errorResult.IsValid)
                throw new InvalidOperationException("Result cannot be succeed");

            List<ValidationFailure> validationErrors = errorResult.Errors;

            // make from validation errors (FluentValidation) to errors 
            var errorsForEnvelope = from validationError in validationErrors
                         let serializedErrorAsMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(serializedErrorAsMessage)
                         let resposeError = new ErrorForEnvelope(error.Code, error.Message, validationError.PropertyName)
                         select resposeError;

            // errors in envelope
            ResponseEnvelope envelope = ResponseEnvelope.Error(errorsForEnvelope);

            return new ObjectResult(envelope)
            {
                StatusCode = GetStatusCodeForValidationErrors(validationErrors),
            };
        }

        private static int GetStatusCodeForErrorType(ErrorType errorType)
        {
            int statusCode = errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            return statusCode;
        }

        private static int GetStatusCodeForValidationErrors(IEnumerable<ValidationFailure> validationErrors)
        {
            var errors = from validationError in validationErrors
                         let serializedErrorAsMessage = validationError.ErrorMessage
                         let error = Error.Deserialize(serializedErrorAsMessage)
                         select error;

            var distincctErrorTypes = errors
                .Select(x => x.Type)
                .Distinct()
                .ToList();

            int statusCode = distincctErrorTypes.Count > 1
                ? StatusCodes.Status500InternalServerError
                : GetStatusCodeForErrorType(distincctErrorTypes.First());

            return statusCode;
        }
    }
}
