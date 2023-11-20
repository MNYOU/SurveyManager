using System.Text.Json.Serialization;
using Application.Common.Result;
using Application.Common.Result.Validation;

namespace Infrastructure.Common.Result;

public interface IResult
{
    ResultStatus Status { get; }
    
    IEnumerable<string> Errors { get; }
    
    List<ValidationError> ValidationErrors { get; }
    
    [JsonIgnore]
    Type ValueType { get; }
    
    object GetValue();
}