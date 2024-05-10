using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class FeedService(FeedRepository feedRepository)
{
	public async Task AddAsync(Feed feed)
	{
		await feedRepository.AddAsync(feed);
		await feedRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(Feed feed)
	{
		feedRepository.Delete(feed);
		await feedRepository.SaveChangesAsync();
	}

	public IEnumerable<Feed> GetAll()
	{
		return feedRepository.GetAll();
	}

	public Feed GetById(int id)
	{
		return feedRepository.GetById(id);
	}

	public Feed? GetByUrl(string url)
	{
		return feedRepository.GetByUrl(url);
	}

	public async Task UpdateAsync(Feed feed)
	{
		var entity = feedRepository.GetById(feed.Id);

		feedRepository.Update(entity);
		await feedRepository.SaveChangesAsync();
	}
}
