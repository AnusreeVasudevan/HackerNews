using Backend.Models;

namespace Backend.Services;

public interface IHackerNewsClient
{
    Task<IReadOnlyList<int>> GetNewStoryIdsAsync();
    Task<Story?> GetStoryAsync(int id);
}
