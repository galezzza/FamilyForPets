using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FamilyForPets.Shared
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? valueName = null)
            {
                string label = valueName ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is invalid.");
            }

            public static Error CannotBeEmpty(string? stringName = null)
            {
                string label = stringName ?? "value";
                return Error.Validation("value.is.invalid", $"{label} cannot be empty.");
            }

            public static Error ValueIsRequired(string? stringName = null)
            {
                string label = stringName == null ? string.Empty : stringName + " ";
                return Error.Validation("value.is.invalid", $"Invalid {label}lenght");
            }

            public static Error NotFound(ErrorNotFoundObjectDto? dto)
            {
                string label = dto == null ? string.Empty : "for { dto.ObjectName}: {dto.ObjectValue}";
                return Error.NotFound("record.not.found", $"record not found.");
            }

            public static Error Failure()
            {
                return Error.Failure("unexpected.error", "An unexpected error occurred.");
            }

            public static Error Conflict()
            {
                return Error.Conflict("conflict.occured", "A conflict occurred.");
            }

            public record ErrorNotFoundObjectDto()
            {
                public object? ObjectName { get; set; }

                public object? ObjectValue { get; set; }
            }
        }
    }
}
