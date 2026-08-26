
namespace PulseFit.BLL.Models;

public class Results
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public string? Property { get; set; } = default!;

    public static Results Success() => new() { IsSuccess = true };
    public static Results Failure(string error, string code) => new() { IsSuccess = false, Error = error, Property = code };
}

public class Results<T> : Results
{
    public T? Data { get; private set; }

    public static Results<T> Success(T data) => new() { IsSuccess = true, Data = data };

    public new static Results<T> Failure(string error, string code) => new() { IsSuccess = false, Error = error, Property = code };

    //public static implicit operator Results<T>(Results result)
    //{
    //    return new Results<T>
    //    {
    //        IsSuccess = result.IsSuccess,
    //        Error = result.Error,
    //        Property = result.Property
    //    };
    //}
}