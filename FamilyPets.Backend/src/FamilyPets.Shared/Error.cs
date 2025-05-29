using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FamilyForPets.Shared
{
    public record Error
    {
        public const string SEPARATOR = "||";

        public string Code { get; }

        public string Message { get; }

        public ErrorType Type { get; }

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error Validation(string code, string message)
            => new Error(code, message, ErrorType.Validation);

        public static Error NotFound(string code, string message)
            => new Error(code, message, ErrorType.NotFound);

        public static Error Failure(string code, string message)
            => new Error(code, message, ErrorType.Failure);

        public static Error Conflict(string code, string message)
            => new Error(code, message, ErrorType.Conflict);

        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type);
        }

        public static Error Deserialize(string serializedString)
        {
            string[] parts = serializedString.Split(SEPARATOR);
            if (parts.Length < 3)
                throw new ArgumentException("Invaid serialized format");

            if (Enum.TryParse<ErrorType>(parts[2], out ErrorType type) == false)
                throw new ArgumentException("Invaid serialized format");

            return new Error(parts[0], parts[1], type);
        }

    }

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Conflict,
    }
}
