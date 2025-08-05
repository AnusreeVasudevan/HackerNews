using Backend.Models;
using Backend.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Backend.Tests;

public class StoryServiceTests
{
    [Fact]
    public async Task GetStoriesAsync_FiltersAndPaginates()
    {
        var client = new Mock<IHackerNewsClient>();
        client.Setup(c => c.GetNewStoryIdsAsync()).ReturnsAsync(new List<int> { 1, 2, 3 });
        client.Setup(c => c.GetStoryAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => new Story { Id = id, Title = $"Title {id}", Url = "http://test", By = $"Author{id}" });

        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new StoryService(client.Object, cache);

        var result = await service.GetStoriesAsync(1, 2, "Title 1");

        Assert.Equal(1, result.Total);
        Assert.Single(result.Items);
        Assert.Equal(1, result.Items[0].Id);
    }

    [Fact]
    public async Task GetStoriesAsync_UsesCache()
    {
        var client = new Mock<IHackerNewsClient>();
        client.Setup(c => c.GetNewStoryIdsAsync()).ReturnsAsync(new List<int> { 1 });
        client.Setup(c => c.GetStoryAsync(1)).ReturnsAsync(new Story { Id = 1, Title = "T", Url = "http://", By = "A" });

        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new StoryService(client.Object, cache);

        await service.GetStoriesAsync(1, 1, null);
        await service.GetStoriesAsync(1, 1, null);

        client.Verify(c => c.GetNewStoryIdsAsync(), Times.Once);
        client.Verify(c => c.GetStoryAsync(1), Times.Once);
    }
}
