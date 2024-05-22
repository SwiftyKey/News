using CodeHollow.FeedReader;
using Microsoft.EntityFrameworkCore;
using News.Models.Common;
using News.Models.Entities;
using News.Models.Repositories;
using News.Utilities;
using News.ViewModels.Services;
using News.Views.Windows;
using System.Collections.ObjectModel;

namespace News.ViewModels;

public class ApplicationVM : BaseChanged
{
	public static Models.Repositories.AppContext DB { get; set; } = new();
	public static FeedService FeedService { get; set; } = new(new FeedRepository(DB));
	public static SourceService SourceService { get; set; } = new(new SourceRepository(DB));

	public ObservableCollection<Source> Sources { get; set; } = [];
	public ObservableCollection<Models.Entities.Feed> Feeds { get; set; } = [];

	public ApplicationVM()
	{
		DB.Feeds.Load();
		DB.Sources.Load();

		Feeds = DB.Feeds.Local.ToObservableCollection();
		Sources = DB.Sources.Local.ToObservableCollection();
	}

	private RelayCommand? addSourceCommand;
	public RelayCommand AddSourceCommand
	{
		get
		{
			return addSourceCommand ??= new RelayCommand((o) =>
			{
				AddSourcesWindow addSourcesWindow = new();

				if (addSourcesWindow.ShowDialog() == true)
				{
					string sourceUrl = addSourcesWindow.TBSourceLink.Text;

					var sources = FeedReader.GetFeedUrlsFromUrlAsync(sourceUrl).Result;

					if (sources.Any())
						sourceUrl = sources.First().Url;

					if (Sources.Any(s => s.Url == sourceUrl)) return;

					var reader = FeedReader.ReadAsync(sourceUrl);
					reader.ConfigureAwait(false);
					var result = reader.Result;

					var source = new Source
					{
						Url = sourceUrl,
						Title = result.Title,
						Description = result.Description,
						ImageUrl = result.ImageUrl
					};

					_ = SourceService.AddAsync(source);

					_ = FeedService.AddRangeAsyncBySource(source);
				}
			});
		}
	}

	private RelayCommand? removeSourceCommand;
	public RelayCommand RemoveSourceCommand
	{
		get
		{
			return removeSourceCommand ??= new RelayCommand((selectedItem) =>
			{
				Source? source = selectedItem as Source;
				if (source is null) return;
				_ = SourceService.DeleteAsync(source);
			});
		}
	}

	private RelayCommand? viewFeedCommand;
	public RelayCommand? ViewFeedCommand
	{
		get
		{
			return viewFeedCommand ??= new RelayCommand((selectedItem) => 
			{
				if (selectedItem is null) return;

				Models.Entities.Feed? feed = selectedItem as Models.Entities.Feed;
				FeedWindow feedWindow = new (new FeedWindowVM(feed));
				feedWindow.Show();
			});
		}
	}
}
