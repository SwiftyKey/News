using CodeHollow.FeedReader;
using News.ViewModels.Services;

namespace News.Models;

public class Observer(FeedService feedService)
{
	public TimeOnly UpdateFreq { get; set; } = new TimeOnly(hour : 0, minute : 1);
	public FeedService FeedService { get; set; } = feedService;

	public async Task Update()
	{
		/*foreach (var source in Sources)
		{
			var feed = await FeedReader.ReadAsync(source.Url);

			foreach (var item in feed.Items)
			{
				var result = FeedService.GetByUrl(item.Link);

				if (result is null)
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

		Thread.Sleep(UpdateFreq.ToTimeSpan());*/
	}
}
