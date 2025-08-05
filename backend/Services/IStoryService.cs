using Backend.Models;

namespace Backend.Services;

public interface IStoryService
{
    Task<PaginatedResponse<Story>> GetStoriesAsync(int page, int pageSize, string? search);
}
