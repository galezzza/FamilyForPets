
using System.Net;

namespace FamilyForPets.API.ResponsesCommonLogic.EndpointResults
{
    public class SuccesResult<TValue> : IResult
    {
        private readonly TValue _value;

        public SuccesResult(TValue value)
        {
            _value = value;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext);

            ResponseEnvelope envelope = ResponseEnvelope.Correct(_value);

            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            return httpContext.Response.WriteAsJsonAsync(envelope);
        }
    }
}
