namespace Common.Communication;

public class StandardResponse<T> : BaseResponse where T: class
{
    public T? Obj { get; set; }

    private StandardResponse(T? obj, bool success, string? message) : base(success, message)
    {
        Obj = obj;
    }

    /// <summary>
    /// Creates a success response.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Response.</returns>
    public StandardResponse(T? obj) : this(obj, true, null)
    {
    }

    /// <summary>
    /// Returns a error message.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>Response.</returns>
    public StandardResponse(string? message) : this(null, false, message)
    {
        Message = message + "\nPlease contact galactic government to solve this issue.";
    }
}