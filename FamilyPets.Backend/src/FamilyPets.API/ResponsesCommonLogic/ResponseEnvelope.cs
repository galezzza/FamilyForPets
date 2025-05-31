namespace FamilyForPets.API.ResponsesCommonLogic
{
    public record ErrorForEnvelope(string? ErrorCode, string? ErrorMessage, string? InvalidField);

    public record ResponseEnvelope
    {
        public object? Result { get; }

        public List<ErrorForEnvelope> Errors { get; }
        public DateTime TimeGenerated { get; }

        private ResponseEnvelope(object? result, IEnumerable<ErrorForEnvelope> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
        }

        public static ResponseEnvelope Correct(object? result = null) =>
            new ResponseEnvelope(result, []);

        public static ResponseEnvelope Error(IEnumerable<ErrorForEnvelope> errors) =>
            new ResponseEnvelope(null, errors);

    }

    public record ResponseEnvelope<T>
    {
        public T? Result { get; }

        public List<ErrorForEnvelope> Errors { get; }
        public DateTime TimeGenerated { get; }

        private ResponseEnvelope(T? result, IEnumerable<ErrorForEnvelope> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
        }

        public static ResponseEnvelope<T> Correct(T? result = default) =>
            new ResponseEnvelope<T>(result, []);

        public static ResponseEnvelope<T> Error(IEnumerable<ErrorForEnvelope> errors) =>
            new ResponseEnvelope<T>(default, errors);

    }
}
