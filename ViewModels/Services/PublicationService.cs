using CodeHollow.FeedReader;
using News.Models.Entities;
using News.Models.Repositories;

/**
	\brief Пространство имен, в котором содержатся классы для работы с сервисами
	\param Содержит классы:
		@ref PublicationService
		@ref SourceService
*/
namespace News.ViewModels.Services;

/**
	\brief Класс, предназначенный для взаимодействия программы с репозиторием публикаций
	
	Работа с таблицей Publications происходит через данный сервис
*/
public class PublicationService(PublicationRepository publicationRepository)
{
	/**
		\brief Асинхронный метод, добавляющий публикацию в базу данных
		\param[in] publication Добавляемая публикация
		\return Добавленная публикация
	*/
	public async Task<Publication> AddAsync(Publication publication)
	{
		var addedPublication = await publicationRepository.AddAsync(publication);
		await publicationRepository.SaveChangesAsync();
		return addedPublication;
	}

	/**
		\brief Асинхронный метод, добавляющий несколько публикаций в базу данных
		\param[in] publications Коллекция добавляемых объектов
	*/
	public async Task AddRangeAsync(IEnumerable<Publication> publications)
	{
		await publicationRepository.AddRangeAsync(publications);
		await publicationRepository.SaveChangesAsync();
	}

	/**
		\brief Асинхронный метод, добавляющий публикации нового источника
		\param[in] source Новый источник
	*/
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

	/**
		\brief Асинхронный метод, удаляющий указанную публикацию из базы данных
		\param[in] publication Удаляемая публикация
	*/
	public async Task DeleteAsync(Publication publication)
	{
		publicationRepository.Delete(publication);
		await publicationRepository.SaveChangesAsync();
	}

	/**
		\brief Метод, возвращающий все публикации из таблицы Publications
		\return Коллекция публикаций
	*/
	public IEnumerable<Publication> GetAll() => publicationRepository.GetAll();

	/**
		\brief Метод, возвращающий публикацию по ее первичному ключу
		\param[in] id Первичный ключ требуемой публикации
		\return Полученная публикация
	*/
	public Publication GetById(int id) => publicationRepository.GetById(id);

	/**
		\brief Метод, получения публикации по ее ссылке
		\param[in] url ссылка на публикацию
		\return Полученная публикация
	*/
	public Publication? GetByUrl(string url) => publicationRepository.GetByUrl(url);

	/**
		\brief Асинхронный метод, обновляющий указанную публикацию в базе данных
		\param[in] publication Публикация, которую надо изменить
	*/
	public async Task UpdateAsync(Publication publication)
	{
		var entity = publicationRepository.GetById(publication.Id);

		publicationRepository.Update(entity);
		await publicationRepository.SaveChangesAsync();
	}
}
