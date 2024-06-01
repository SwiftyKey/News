using CodeHollow.FeedReader;
using Microsoft.Toolkit.Uwp.Notifications;
using News.Models.Entities;
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

	/// Сервис для таблицы Publications
	public PublicationService PublicationService { get; set; } = new(new PublicationRepository(ApplicationVM.DB));

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
				var result = PublicationService.GetByUrl(item.Link);

				if (result is null)
				{
					var newPublication = new Publication()
					{
						Title = item.Title,
						Link = item.Link,
						PublishingDate = item.PublishingDate,
						Source = source
					};

					var addedPublication = await PublicationService.AddAsync(newPublication);

					if (SettingsVM.AppSettings.NotificationsOn)
						await ViewNotification(addedPublication);
				}
			}
		}
	}

	/**
		\brief Асинхронный метод, для уведомления пользователя о вышедшей публикации источника
		\param[in] publication Вышедшая публикация
	*/
	public static async Task ViewNotification(Publication publication)
	{
		var builder = new ToastContentBuilder()
			.AddArgument("Action", "ViewPublication")
			.AddArgument("PublicationId", publication.Id)
			.AddText(publication.Source.Title)
			.AddText(publication.Title);
		builder.Show();
	}
}
