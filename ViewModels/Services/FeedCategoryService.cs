using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class feedCategoryService(FeedCategoryRepository fcRepository)
{
	public async Task AddAsync(FeedCategory feedCategory)
	{
		await fcRepository.AddAsync(feedCategory);
		await fcRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(FeedCategory feedCategory)
	{
		fcRepository.Delete(feedCategory);
		await fcRepository.SaveChangesAsync();
	}

	public IEnumerable<FeedCategory> GetAll()
	{
		return fcRepository.GetAll();
	}

	public FeedCategory GetById(int id)
	{
		return fcRepository.GetById(id);
	}

	public async Task UpdateAsync(FeedCategory feedCategory)
	{
		var entity = fcRepository.GetById(feedCategory.Id);

		fcRepository.Update(entity);
		await fcRepository.SaveChangesAsync();
	}
}