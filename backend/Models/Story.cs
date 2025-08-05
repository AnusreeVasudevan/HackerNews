namespace Backend.Models;

public record Story
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Url { get; init; }
    public string? By { get; init; }
}
