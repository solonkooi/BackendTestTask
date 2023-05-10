namespace BackendTestTask.Business.Models;

public class StatusResponseText
{
    public string? StackTrace { get; set; }
    public string? Message { get; set; }
    public string Query { get; set; }
    public string Body { get; set; }
    public Dictionary<string, string> Headers { get; set; }
}