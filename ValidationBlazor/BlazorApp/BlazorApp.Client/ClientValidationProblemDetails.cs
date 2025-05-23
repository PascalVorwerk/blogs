namespace BlazorApp.Client;

public class ClientValidationProblemDetails
{
    public Dictionary<string, string[]> Errors { get; set; } = new();

    public int Status { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
}