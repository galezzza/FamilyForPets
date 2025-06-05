using System.Net;
using FamilyForPets.API.Responses;
using FamilyForPets.Shared;
using Microsoft.AspNetCore.Http;

namespace FamilyForPets.API.Responses.EndpointResults
{
    public class ErrorsResult : IResult
    {
        private readonly ErrorList _errors;

        public ErrorsResult(ErrorList errors)
        {
            _errors = errors;
        }

        public ErrorsResult(Error error)
        {
            _errors = error.ToErrorList();
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            if(_errors.Any() == false)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            else
            {
                httpContext.Response.StatusCode = GetStatusCodeForErrorList(_errors);
            }

            ResponseEnvelope envelope = ResponseEnvelope.Error(_errors);

            return httpContext.Response.WriteAsJsonAsync(envelope);
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
