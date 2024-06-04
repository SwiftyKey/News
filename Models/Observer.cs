using CodeHollow.FeedReader;
using Microsoft.Toolkit.Uwp.Notifications;
using News.Models.Entities;
using News.Models.Repositories;
using News.ViewModels;
using News.ViewModels.Services;

namespace News.Models;

public class Observer
{
	public TimeOnly UpdateFreq { get; set; } = new TimeOnly(hour : 0, minute : 10);
	public PublicationService PublicationService { get; set; } = new(new PublicationRepository(ApplicationVM.DB));

	public async Task Update()
	{
		if (ApplicationVM.CurrentUser is null) return;

		foreach (var source in ApplicationVM.CurrentUser.Sources)
		{
			var reader = await FeedReader.ReadAsync(source.Url);

			foreach (var item in reader.Items.Reverse())
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

					if (ApplicationVM.CurrentUser.NotificationsOn)
						await ViewNotification(addedPublication);
				}
			}
		}
	}

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
