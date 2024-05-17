﻿using CodeHollow.FeedReader;
using Microsoft.EntityFrameworkCore;
using News.Models.Common;
using News.Models.Entities;
using News.Models.Repositories;
using News.Utilities;
using News.ViewModels.Services;
using News.Views.Windows;
using System.Collections.ObjectModel;

namespace News.ViewModels;

public class ApplicationVM : BaseViewModel
{
	public static Models.Repositories.AppContext DB { get; set; } = new();
	public static UserService UserService { get; set; } = new(new UserRepository(DB));
	public static FeedService FeedService { get; set; } = new(new FeedRepository(DB));
	public static SourceService SourceService { get; set; } = new(new SourceRepository(DB));
	public static User? CurrentUser { get; set; }
	public static bool NotificationsOn { get; set; } = false;

	public ObservableCollection<Source> Sources { get; set; } = [];
	public ObservableCollection<Models.Entities.Feed> Feeds { get; set; } = [];
	public ObservableCollection<Models.Entities.Feed> FeedsReadLater { get; set; } = [];
	public ObservableCollection<Models.Entities.Feed> FeedsFavourites { get; set; } = [];

	public ApplicationVM()
	{
		DB.Users.Load();
		DB.Feeds.Load();
		DB.Sources.Load();

		Feeds = DB.Feeds.Local.ToObservableCollection();
		Sources = DB.Sources.Local.ToObservableCollection();
	}

	Source? selectedSource;
	public Source? SelectedSource
	{
		get => selectedSource;
		set
		{
			selectedSource = value;
			OnPropertyChanged(nameof(SelectedSource));
		}
	}

	Models.Entities.Feed? selectedFeed;
	public Models.Entities.Feed? SelectedFeed
	{
		get => selectedFeed;
		set
		{
			selectedFeed = value;
			OnPropertyChanged(nameof(SelectedFeed));
		}
	}

	private RelayCommand? addSourceCommand;
	public RelayCommand AddSourceCommand
	{
		get
		{
			return addSourceCommand ??= new RelayCommand((o) =>
				{
					AddSourcesWindow addSourcesWindow = new(new Source());

					if (addSourcesWindow.ShowDialog() == true)
					{
						Source source = addSourcesWindow.Source;

						string sourceUrl = "";
						var sources = FeedReader.GetFeedUrlsFromUrlAsync(addSourcesWindow.TBSourceLink.Text).Result;

						if (!sources.Any())
							sourceUrl = addSourcesWindow.TBSourceLink.Text;
						else
							sourceUrl = sources.First().Url;

						var reader = FeedReader.ReadAsync(sourceUrl).Result;
						source.Url = sourceUrl;
						source.Title = reader.Title;
						source.Description = reader.Description;
						source.ImageUrl = reader.ImageUrl;

						_ = SourceService.AddAsync(source);
						_ = UserService.AddSourceByUserId(source, CurrentUser.Id);
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
			return removeSourceCommand ??
				(removeSourceCommand = new RelayCommand(obj =>
				{
					Source? source = obj as Source;
					if (source != null)
						Sources.Remove(source);
				},
				(obj) => Sources.Count > 0));
		}
	}
}