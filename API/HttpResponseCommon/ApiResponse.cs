namespace ThunderAPI.Api.HttpResponseCommon;
public class ApiResponse
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public object Data { get; private set; }

    private ApiResponse(bool isSuccess, string message, object data)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static ApiResponse FromSuccess(object data, string message = "Request successful")
    {
        return new ApiResponse(true, message, data);
    }

    public static ApiResponse FromFailure(string message)
    {
        return new ApiResponse(false, message, null);
    }
}
