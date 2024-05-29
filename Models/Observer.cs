using CodeHollow.FeedReader;
using Microsoft.Toolkit.Uwp.Notifications;
using News.Models.Repositories;
using News.ViewModels;
using News.ViewModels.Services;

/**
	\brief Пространство имен, в котором содержится класс Observer
	\param Содержит класс:
		@ref Observer
*/
namespace News.Models;

/**
	\brief Класс, предназначенный для своевременного обновления текущих публикаций источников
	
	Наследуется от BaseChanged
*/
public class Observer
{
	/// Частота обновления публикаций
	public TimeOnly UpdateFreq { get; set; } = new TimeOnly(hour : 0, minute : 10);

	/// Сервис для таблицы Feeds
	public FeedService FeedService { get; set; } = new(new FeedRepository(ApplicationVM.DB));

	/**
		\brief Асинхронный метод, проверяющий последние вышедшие публикации источников
	*/
	public async Task Update()
	{
		foreach (var source in ApplicationVM.Sources)
		{
			var reader = await FeedReader.ReadAsync(source.Url);

			foreach (var item in reader.Items)
			{
				var result = FeedService.GetByUrl(item.Link);

				if (result is null)
				{
					var newFeed = new Entities.Feed()
					{
						Title = item.Title,
						Link = item.Link,
						PublishingDate = item.PublishingDate,
						Source = source
					};

					var addedFeed = await FeedService.AddAsync(newFeed);

					if (SettingsVM.AppSettings.NotificationsOn)
						await ViewNotification(addedFeed);
				}
			}
		}
	}

	/**
		\brief Асинхронный метод, для уведомления пользователя о вышедшей публикации источника
		\param[in] feed Вышедшая публикация
	*/
	public static async Task ViewNotification(Entities.Feed feed)
	{
		var builder = new ToastContentBuilder()
			.AddArgument("Action", "ViewFeed")
			.AddArgument("FeedId", feed.Id)
			.AddText(feed.Source.Title)
			.AddText(feed.Title);
		builder.Show();
	}
}
