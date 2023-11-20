namespace Infrastructure.Common.Result;

public enum ResultStatus
{
    /// <summary>
    ///     Corresponds to (<see cref="StatusCodes.Status200OK" /> by default).
    /// </summary>
    Ok = 200,

    /// <summary>
    ///     Corresponds to (<see cref="StatusCodes.Status422UnprocessableEntity" /> by default).
    /// </summary>
    Error = 422,

    /// <summary>
    ///     Corresponds to (<see cref="StatusCodes.Status403Forbidden" /> by default).
    /// </summary>
    Forbidden = 403,

    /// <summary>
    ///     Corresponds to (<see cref="StatusCodes.Status401Unauthorized" /> by default).
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    ///     Corresponds to (<see cref="StatusCodes.Status400BadRequest" /> by default).
    /// </summary>
    Invalid = 400,

    /// <summary>
    ///     Corresponds to (<see cref="StatusCodes.Status404NotFound" /> by default).
    /// </summary>
    NotFound = 404
}