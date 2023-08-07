namespace WebLibrary.API.Models;

public class CustomBadRequestResult
{
    public int StatusCode { get; set; }

    public string Title { get; set; } = null!;

    public object Errors { get; set; } = null!;
}