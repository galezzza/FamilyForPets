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

            ResponseEnvelope envelope = ResponseEnvelope.Error(error.ToErrorList());

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        public static ActionResult ToResponseFromErrorList(this ErrorList errors)
        {
            if (errors.Any() == false)
            {
                return new ObjectResult(ResponseEnvelope.Error(errors))
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            var statusCode = GetStatusCodeForErrorList(errors);

            ResponseEnvelope envelope = ResponseEnvelope.Error(errors);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        public static ActionResult ToResponseFromSuccessResult<T>(this Result<T, ErrorList> correctResult)
        {
            if (correctResult.IsFailure)
                throw new InvalidOperationException("Result cannot be failed");

            ResponseEnvelope<T> envelope = ResponseEnvelope<T>.Correct(correctResult.Value);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status200OK,
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

        private static int GetStatusCodeForErrorList(ErrorList errors)
        {
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
