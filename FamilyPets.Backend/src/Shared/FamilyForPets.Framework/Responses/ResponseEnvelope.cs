using FamilyForPets.SharedKernel;

namespace FamilyForPets.Framework.Responses
{
    public record ResponseEnvelope
    {
        public object? Result { get; }

        public ErrorList? Errors { get; }
        public DateTime TimeGenerated { get; }

        private ResponseEnvelope(object? result, ErrorList? errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.Now;
        }

        public static ResponseEnvelope Correct(object? result = null) =>
            new ResponseEnvelope(result, null);

        public static ResponseEnvelope Error(ErrorList errors) =>
            new ResponseEnvelope(null, errors);

    }

    public record ResponseEnvelope<T>
    {
        public T? Result { get; }

        public ErrorList? Errors { get; }
        public DateTime TimeGenerated { get; }

        private ResponseEnvelope(T? result, ErrorList? errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.Now;
        }

        public static ResponseEnvelope<T> Correct(T? result = default) =>
            new ResponseEnvelope<T>(result, null);

        public static ResponseEnvelope<T> Error(ErrorList errors) =>
            new ResponseEnvelope<T>(default, errors);

    }
}
