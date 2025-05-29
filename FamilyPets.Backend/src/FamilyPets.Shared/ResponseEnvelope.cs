namespace FamilyForPets.Shared
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
}
