using Backend.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services;

public class StoryService : IStoryService
{
    private const string CacheKey = "hn_newstories";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);
    private readonly IHackerNewsClient _client;
    private readonly IMemoryCache _cache;

    public StoryService(IHackerNewsClient client, IMemoryCache cache)
    {
        _client = client;
        _cache = cache;
    }

    private async Task<IReadOnlyList<Story>> GetAllStoriesAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out List<Story>? stories))
        {
            var ids = await _client.GetNewStoryIdsAsync();
            var tasks = ids.Select(id => _client.GetStoryAsync(id));
            var results = await Task.WhenAll(tasks);
            stories = results
                .Where(s => s != null && !string.IsNullOrWhiteSpace(s.Url))
                .Select(s => s!)
                .ToList();
            _cache.Set(CacheKey, stories, CacheDuration);
        }
        return stories;
    }

    public async Task<PaginatedResponse<Story>> GetStoriesAsync(int page, int pageSize, string? search)
    {
        var stories = await GetAllStoriesAsync();
        if (!string.IsNullOrWhiteSpace(search))
        {
            stories = stories
                .Where(s => (s.Title?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (s.By?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
        }
        var total = stories.Count;
        var items = stories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedResponse<Story>(items, total);
    }
}
