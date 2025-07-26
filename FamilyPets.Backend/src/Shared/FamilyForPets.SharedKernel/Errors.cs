using static FamilyForPets.SharedKernel.Errors.General;

namespace FamilyForPets.SharedKernel
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

        public static class Database
        {
            public static Error TransactionConflict(string? operationName = null)
            {
                string label = operationName == null ? string.Empty : $"with operation {operationName}";
                return Error.Conflict("database.transaction.conflict", $"A conflict occurred {label}.");
            }
        }
    }
}
