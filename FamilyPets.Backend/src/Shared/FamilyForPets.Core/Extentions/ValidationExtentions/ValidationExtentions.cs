using CSharpFunctionalExtensions;
using FamilyForPets.SharedKernel;
using FluentValidation.Results;

namespace FamilyForPets.Core.Extentions.ValidationExtentions
{
    public static class ValidationExtentions
    {
        public static ErrorList ToErrorListFromValidationResult(this ValidationResult validationResult)
        {
            List<ValidationFailure> validationErrors = validationResult.Errors;

            IEnumerable<Error> errors = from validationError in validationErrors
                                        let serializedErrorAsMessage = validationError.ErrorMessage
                                        let error = Error.Deserialize(serializedErrorAsMessage)
                                        select Error.Validation(error.Code, error.Message, validationError.PropertyName);

            return new ErrorList(errors);
        }

    }
}
