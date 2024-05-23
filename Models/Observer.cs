using CodeHollow.FeedReader;
using Microsoft.Toolkit.Uwp.Notifications;
using News.Models.Repositories;
using News.ViewModels;
using News.ViewModels.Services;

namespace News.Models;

public class Observer
{
	public TimeOnly UpdateFreq { get; set; } = new TimeOnly(hour : 0, minute : 10);
	public FeedService FeedService { get; set; } = new FeedService(new FeedRepository(ApplicationVM.DB));

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
