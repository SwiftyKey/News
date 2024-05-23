using CodeHollow.FeedReader;
using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class FeedService(FeedRepository feedRepository)
{
	public async Task<Models.Entities.Feed> AddAsync(Models.Entities.Feed feed)
	{
		var addedFeed = await feedRepository.AddAsync(feed);
		await feedRepository.SaveChangesAsync();
		return addedFeed;
	}

	public async Task AddRangeAsync(IEnumerable<Models.Entities.Feed> feeds)
	{
		await feedRepository.AddRangeAsync(feeds);
		await feedRepository.SaveChangesAsync();
	}

	public async Task AddRangeAsyncBySource(Source source)
	{
		await feedRepository.AddRangeAsync(
			FeedReader
			.ReadAsync(source.Url)
			.Result
			.Items
			.Select(item => new Models.Entities.Feed
			{
				Title = item.Title,
				Link = item.Link,
				PublishingDate = item.PublishingDate,
				Source = source
			})
		);
		await feedRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(Models.Entities.Feed feed)
	{
		feedRepository.Delete(feed);
		await feedRepository.SaveChangesAsync();
	}

	public IEnumerable<Models.Entities.Feed> GetAll()
	{
		return feedRepository.GetAll();
	}

	public Models.Entities.Feed GetById(int id)
	{
		return feedRepository.GetById(id);
	}

	public Models.Entities.Feed? GetByUrl(string url)
	{
		return feedRepository.GetByUrl(url);
	}

	public async Task UpdateAsync(Models.Entities.Feed feed)
	{
		var entity = feedRepository.GetById(feed.Id);

		feedRepository.Update(entity);
		await feedRepository.SaveChangesAsync();
	}
}
