using CodeHollow.FeedReader;
using News.Models.Entities;
using News.Models.Repositories;

/**
	\brief Пространство имен, в котором содержатся классы для работы с сервисами
	\param Содержит классы:
		@ref FeedService
		@ref SourceService
*/
namespace News.ViewModels.Services;

/**
	\brief Класс, предназначенный для взаимодействия программы с репозиторием публикаций
	
	Работа с таблицей Feeds происходит через данный сервис
*/
public class FeedService(FeedRepository feedRepository)
{
	/**
		\brief Асинхронный метод, добавляющий публикацию в базу данных
		\param[in] feed Добавляемая публикация
		\return Добавленная публикация
	*/
	public async Task<Models.Entities.Feed> AddAsync(Models.Entities.Feed feed)
	{
		var addedFeed = await feedRepository.AddAsync(feed);
		await feedRepository.SaveChangesAsync();
		return addedFeed;
	}

	/**
		\brief Асинхронный метод, добавляющий несколько публикаций в базу данных
		\param[in] feeds Коллекция добавляемых объектов
	*/
	public async Task AddRangeAsync(IEnumerable<Models.Entities.Feed> feeds)
	{
		await feedRepository.AddRangeAsync(feeds);
		await feedRepository.SaveChangesAsync();
	}

	/**
		\brief Асинхронный метод, добавляющий публикации нового источника
		\param[in] source Новый источник
	*/
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

	/**
		\brief Асинхронный метод, удаляющий указанную публикацию из базы данных
		\param[in] feed Удаляемая публикация
	*/
	public async Task DeleteAsync(Models.Entities.Feed feed)
	{
		feedRepository.Delete(feed);
		await feedRepository.SaveChangesAsync();
	}

	/**
		\brief Метод, возвращающий все публикации из таблицы Feeds
		\return Коллекция публикаций
	*/
	public IEnumerable<Models.Entities.Feed> GetAll()
	{
		return feedRepository.GetAll();
	}

	/**
		\brief Метод, возвращающий публикацию по ее первичному ключу
		\param[in] id Первичный ключ требуемой публикации
		\return Полученная публикация
	*/
	public Models.Entities.Feed GetById(int id)
	{
		return feedRepository.GetById(id);
	}

	/**
		\brief Метод, получения публикации по ее ссылке
		\param[in] url ссылка на публикацию
		\return Полученная публикация
	*/
	public Models.Entities.Feed? GetByUrl(string url)
	{
		return feedRepository.GetByUrl(url);
	}

	/**
		\brief Асинхронный метод, обновляющий указанную публикацию в базе данных
		\param[in] feed Публикация, которую надо изменить
	*/
	public async Task UpdateAsync(Models.Entities.Feed feed)
	{
		var entity = feedRepository.GetById(feed.Id);

		feedRepository.Update(entity);
		await feedRepository.SaveChangesAsync();
	}
}
