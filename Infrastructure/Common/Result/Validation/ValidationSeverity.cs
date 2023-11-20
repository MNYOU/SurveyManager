using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Common.Result.Validation;

public enum ValidationSeverity
{
    [Display(Name = nameof(Error))] Error = 0,
    [Display(Name = nameof(Warning))] Warning = 1,
    [Display(Name = nameof(Info))] Info = 2
}