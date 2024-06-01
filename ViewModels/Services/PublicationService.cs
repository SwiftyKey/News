using CodeHollow.FeedReader;
using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class PublicationService(PublicationRepository publicationRepository)
{
	public async Task<Publication> AddAsync(Publication publication)
	{
		var addedPublication = await publicationRepository.AddAsync(publication);
		await publicationRepository.SaveChangesAsync();
		return addedPublication;
	}

	public async Task AddRangeAsync(IEnumerable<Publication> publications)
	{
		await publicationRepository.AddRangeAsync(publications);
		await publicationRepository.SaveChangesAsync();
	}

	public async Task AddRangeAsyncBySource(Source source)
	{
		await publicationRepository.AddRangeAsync(
			FeedReader
			.ReadAsync(source.Url)
			.Result
			.Items
			.Select(item => new Publication
			{
				Title = item.Title,
				Link = item.Link,
				PublishingDate = item.PublishingDate,
				Source = source
			})
		);
		await publicationRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(Publication publication)
	{
		publicationRepository.Delete(publication);
		await publicationRepository.SaveChangesAsync();
	}

	public IEnumerable<Publication> GetAll() => publicationRepository.GetAll();

	public Publication GetById(int id) => publicationRepository.GetById(id);

	public Publication? GetByUrl(string url) => publicationRepository.GetByUrl(url);

	public async Task UpdateAsync(Publication publication)
	{
		var entity = publicationRepository.GetById(publication.Id);

		publicationRepository.Update(entity);
		await publicationRepository.SaveChangesAsync();
	}
}
