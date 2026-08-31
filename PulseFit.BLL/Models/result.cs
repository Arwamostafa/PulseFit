
namespace PulseFit.BLL.Models;

public class result
{
    public bool IsSuccess { get; set; }

    public string? Error { get; set; }
    public string? Property { get; set; } = default!;

    public static result Success() => new() { IsSuccess = true };
    public static result Failure(string error, string code) => new() { IsSuccess = false, Error = error, Property = code };
}

public class Results<T> : result
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