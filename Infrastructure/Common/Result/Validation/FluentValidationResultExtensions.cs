using System.ComponentModel.DataAnnotations;
using Application.Common.Result.Validation;

namespace Infrastructure.Common.Result.Validation;

public static class FluentValidationResultExtensions
{
    public static List<ValidationError> AsErrors(this ValidationResult valResult)
    {
        throw new NotImplementedException();
        // var resultErrors = new List<ValidationError>();
        //
        // foreach (var valFailure in valResult.Errors)
        // {
        //     resultErrors.Add(new ValidationError()
        //     {
        //         Severity = FromSeverity(valFailure.Severity),
        //         ErrorMessage = valFailure.ErrorMessage,
        //         ErrorCode = valFailure.ErrorCode,
        //         Identifier = valFailure.PropertyName
        //     });
        // }
        //
        // return resultErrors;
    }

    // public static ValidationSeverity FromSeverity(Severity severity)
    public static ValidationSeverity FromSeverity()
    {
        throw new NotImplementedException();
        // switch (severity)
        // {
            // case Severity.Error: return ValidationSeverity.Error;
            // case Severity.Warning: return ValidationSeverity.Warning;
            // case Severity.Info: return ValidationSeverity.Info;
            // default: throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity");
        // }
    }
}