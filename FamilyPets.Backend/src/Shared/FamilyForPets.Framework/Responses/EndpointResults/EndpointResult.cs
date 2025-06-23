using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace FamilyForPets.Framework.Responses.EndpointResults
{
    public class EndpointResult<TValue> : IResult
    {
        private readonly IResult _result;

        public EndpointResult(Result<TValue, Error> result)
        {
            _result = result.IsSuccess
                ? new SuccesResult<TValue>(result.Value)
                : new ErrorsResult(result.Error);
        }

        public EndpointResult(Result<TValue, ErrorList> result)
        {
            _result = result.IsSuccess
                ? new SuccesResult<TValue>(result.Value)
                : new ErrorsResult(result.Error);
        }

        public Task ExecuteAsync(HttpContext httpContext) =>
            _result.ExecuteAsync(httpContext);

        public static implicit operator EndpointResult<TValue>(Result<TValue, Error> result)
            => new EndpointResult<TValue>(result);

        public static implicit operator EndpointResult<TValue>(Result<TValue, ErrorList> result)
            => new EndpointResult<TValue>(result);
    }

    public class EndpointResult : IResult
    {
        private readonly IResult _result;

        public EndpointResult(UnitResult<Error> result)
        {
            _result = result.IsSuccess
                ? new SuccesResult()
                : new ErrorsResult(result.Error);
        }

        public EndpointResult(UnitResult<ErrorList> result)
        {
            _result = result.IsSuccess
                ? new SuccesResult()
                : new ErrorsResult(result.Error);
        }

        public Task ExecuteAsync(HttpContext httpContext) =>
            _result.ExecuteAsync(httpContext);

        public static implicit operator EndpointResult(UnitResult<Error> result)
            => new EndpointResult(result);

        public static implicit operator EndpointResult(UnitResult<ErrorList> result)
            => new EndpointResult(result);
    }
}
