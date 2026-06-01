
namespace PulseFit.BLL.Models;

public class ErrorBody
{
    public ErrorBody(string message, string description, string code)
    {
        Massage = message;
        Description = description;
        Code = code;
    }

    public string Massage { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
}

