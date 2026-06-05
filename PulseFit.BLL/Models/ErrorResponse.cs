namespace PulseFit.BLL.Models;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public ErrorBody Error { get; set; }

    public ErrorResponse(int statusCode, ErrorBody error)
    {
        StatusCode = statusCode;
        Error = error;
    }
}
