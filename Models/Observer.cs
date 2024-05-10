using CodeHollow.FeedReader;
using News.Models.Entities;
using News.ViewModels.Services;

namespace News.Models;

public static class Observer
{
	public static TimeOnly UpdateFreq { get; set; } = new TimeOnly(hour : 0, minute : 30);
	public static User CurrentUser { get; set; }
	public static FeedService FeedService { get; set; }

	public static async Task Update()
	{
		if (CurrentUser.Sources is null) return;

		foreach (var source in CurrentUser.Sources)
		{
			var feed = await FeedReader.ReadAsync(source.Url);
			foreach (var item in feed.Items)
			{
				var result = FeedService.GetByUrl(item.Link);

				if (result is not null) continue;
				else
				{
					var newFeed = new Entities.Feed() {
						Title = item.Title,
						Link = item.Link,
						Description = item.Description,
						PublishingDate = item.PublishingDate,
						Source = source
					};
					await FeedService.AddAsync(newFeed);
				}
			}
		}

		Thread.Sleep(UpdateFreq.ToTimeSpan());
	}
}
