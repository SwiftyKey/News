using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class SourceCategoryService(SourceCategoryRepository scRepository)
{
	public async Task AddAsync(SourceCategory sourceCategory)
	{
		await scRepository.AddAsync(sourceCategory);
		await scRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(SourceCategory sourceCategory)
	{
		scRepository.Delete(sourceCategory);
		await scRepository.SaveChangesAsync();
	}

	public IEnumerable<SourceCategory> GetAll()
	{
		return scRepository.GetAll();
	}

	public SourceCategory GetById(int id)
	{
		return scRepository.GetById(id);
	}

	public async Task UpdateAsync(SourceCategory sourceCategory)
	{
		var entity = scRepository.GetById(sourceCategory.Id);

		scRepository.Update(entity);
		await scRepository.SaveChangesAsync();
	}
}
