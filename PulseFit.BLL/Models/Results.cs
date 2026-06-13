
namespace PulseFit.BLL.Models;

public class Results : BaseResult
{

    public static Results Success() => new() { IsSuccess = true, StatusCode = 200 };
    public static Results NotFound(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 404 };
    public static Results BadRequest(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 400 };
    public static Results ServerError(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 500 };
}


public class Results<T> : BaseResult
{

    public T? Data { get; private set; }

    public static Results<T> Success(T data) => new() { IsSuccess = true, StatusCode = 200, Data = data };

    public static implicit operator Results<T>(Results result)
    {
        return new Results<T>
        {
            IsSuccess = result.IsSuccess,
            Error = result.Error,
            StatusCode = result.StatusCode,
            Data = default
        };
    }
}