namespace PulseFit.BLL.Models;

public class Results
{
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }
    public int StatusCode { get; private set; }

    public static Results Success() => new() { IsSuccess = true, StatusCode = 200 };
    public static Results NotFound(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 404 };
    public static Results BadRequest(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 400 };
    public static Results ServerError(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 500 };
}


public class Results<T>
{
    public bool IsSuccess { get; private set; }
    public string? Error { get; private set; }
    public int StatusCode { get; private set; }
    public T? Data { get; private set; }

    public static Results<T> Success(T data) => new() { IsSuccess = true, StatusCode = 200, Data = data };
    public static Results<T> NotFound(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 404 };
    public static Results<T> BadRequest(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 400 };
    public static Results<T> ServerError(string msg) => new() { IsSuccess = false, Error = msg, StatusCode = 500 };
}
