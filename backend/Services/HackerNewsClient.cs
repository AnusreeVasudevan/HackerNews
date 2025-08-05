using Backend.Models;
using System.Net.Http.Json;

namespace Backend.Services;

public class HackerNewsClient : IHackerNewsClient
{
    private readonly HttpClient _httpClient;

    public HackerNewsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
    }

    public async Task<IReadOnlyList<int>> GetNewStoryIdsAsync()
    {
        return await _httpClient.GetFromJsonAsync<int[]>("newstories.json") ?? Array.Empty<int>();
    }

    public async Task<Story?> GetStoryAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Story>($"item/{id}.json");
    }
}
