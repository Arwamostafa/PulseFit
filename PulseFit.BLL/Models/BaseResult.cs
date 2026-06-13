namespace PulseFit.BLL.Models;

public abstract class BaseResult
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
}

