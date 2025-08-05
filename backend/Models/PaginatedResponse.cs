namespace Backend.Models;

public record PaginatedResponse<T>(IReadOnlyList<T> Items, int Total);
