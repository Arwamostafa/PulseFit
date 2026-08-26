namespace PulseFit.BLL.Models;

public class BaseResult
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public Type? Property { get; set; } = default!;
}

