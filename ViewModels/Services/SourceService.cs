using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class SourceService(SourceRepository sourceRepository)
{
	public async Task AddAsync(Source source)
	{
		await sourceRepository.AddAsync(source);
		await sourceRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(Source source)
	{
		sourceRepository.Delete(source);
		await sourceRepository.SaveChangesAsync();
	}

	public async Task DeleteRangeAsync(IEnumerable<Source> sources)
	{
		sourceRepository.DeleteRange(sources);
		await sourceRepository.SaveChangesAsync();
	}

	public IEnumerable<Source> GetAll()
	{
		return sourceRepository.GetAll();
	}

	public Source GetById(int id)
	{
		return sourceRepository.GetById(id);
	}

	public async Task UpdateAsync(Source source)
	{
		var entity = sourceRepository.GetById(source.Id);

		sourceRepository.Update(entity);
		await sourceRepository.SaveChangesAsync();
	}
}
