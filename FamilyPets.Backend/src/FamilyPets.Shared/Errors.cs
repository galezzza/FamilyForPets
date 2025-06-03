using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static FamilyForPets.Shared.Errors.General;

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
                string label = dto == null ? string.Empty : $"for {dto.ObjectName}: {dto.ObjectValue}";
                return Error.NotFound("record.not.found", $"record not found {label}.");
            }

            public static Error Failure()
            {
                return Error.Failure("unexpected.error", "An unexpected error occurred.");
            }

            public static Error Conflict(string? conflictName = null)
            {
                string label = conflictName == null ? string.Empty : $"for {conflictName}";
                return Error.Conflict("conflict.occurred", $"A conflict occurred {label}.");
            }

            public record ErrorNotFoundObjectDto(object? ObjectName, object? ObjectValue);
        }

        public static class Volunteer
        {
            public static Error Conflict(string? conflictName = null)
            {
                string label = conflictName == null ? string.Empty : $"for {conflictName}";
                return Error.Conflict("volunteer.conflict", $"A conflict occurred {label}.");
            }

            public static Error ConflictAlreadyExists(string? conflictName = null)
            {
                string label = conflictName == null ? string.Empty : $"Volunteer with such {conflictName} already exists.";
                return Error.Conflict("record.conflict.already.exists", $"A conflict occurred. {label}");
            }

            public static Error NotFound(ErrorNotFoundObjectDto? dto)
            {
                string label = dto == null ? string.Empty : $"for volunteer with {dto.ObjectName}: {dto.ObjectValue}";
                return Error.NotFound("record.not.found", $"record not found {label}.");
            }
        }
    }
}
