using CodeHollow.FeedReader;
using Microsoft.EntityFrameworkCore;
using ModernWpf;
using News.Models;
using News.Models.Common;
using News.Models.Entities;
using News.Models.Repositories;
using News.Utilities;
using News.ViewModels.Services;
using News.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml;

namespace News.ViewModels;

public class ApplicationVM : BaseChanged
{
	public static Models.Repositories.AppContext DB { get; set; } = new();
	public static PublicationService PublicationService { get; set; } = new(new PublicationRepository(DB));
	public static SourceService SourceService { get; set; } = new(new SourceRepository(DB));
	public static UserService UserService { get; set; } = new(new UserRepository(DB));

	private ObservableCollection<Publication> publications = [];
	public ObservableCollection<Publication> Publications
	{
		get
		{
			return new ObservableCollection<Publication>
			(
				publications
					.Where(p => CurrentUser.Sources.Contains(p.Source))
					.ToList()
			);
		}
	}

	public static User? CurrentUser { get; set; }
	public static Observer Observer { get; set; } = new();

	public ApplicationVM(string login)
	{
		DB.Publications.Load();
		DB.Sources.Load();
		DB.Users.Load();
		DB.Favourites.Load();
		DB.ReadLater.Load();

		publications = DB.Publications.Local.ToObservableCollection();

		CurrentUser = DB.Users.Include(u => u.Sources).First(u => u.Login == login);
	}

	private RelayCommand? addSourceCommand;
	public RelayCommand AddSourceCommand
	{
		get
		{
			return addSourceCommand ??= new RelayCommand((o) =>
			{
				var addSourcesWindow = new AddSourcesWindow();

				if (addSourcesWindow.ShowDialog() == true)
				{
					string sourceUrl = addSourcesWindow.TBSourceLink.Text;

					try
					{
						var sources = FeedReader.GetFeedUrlsFromUrlAsync(sourceUrl).Result;

						if (CurrentUser.Sources.Any(s => s.Url == sourceUrl)) return;

						var result = FeedReader.ReadAsync(sourceUrl).Result;

						var source = new Source
						{
							Url = sourceUrl,
							Title = result.Title,
							Description = result.Description,
							ImageUrl = result.ImageUrl
						};

						_ = SourceService.AddAsync(source);
						_ = UserService.AddSourceByUserAsync(source, CurrentUser);
						_ = PublicationService.AddRangeAsyncBySource(source);
					}
					catch (XmlException ex)
					{
						MessageBox.Show("Данная RSS-ссылка не поддерживается");
						return;
					}
					catch (Exception ex)
					{
						MessageBox.Show("Не нашлось подходящих результатов");
						return;
					}
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

	private static RelayCommand? viewPublicationCommand;
	public static RelayCommand? ViewPublicationCommand
	{
		get
		{
			return viewPublicationCommand ??= new RelayCommand((selectedItem) => 
			{
				if (selectedItem is null) return;

				Publication? publication = selectedItem as Publication;
				Application.Current.Dispatcher.Invoke(() =>
				{
					PublicationWindow PublicationWindow = new(new PublicationWindowVM(publication));
					PublicationWindow.Show();
				});
			});
		}
	}


	private RelayCommand? themeChangedCommand;
	public RelayCommand? ThemeChangedCommand
	{
		get
		{
			return themeChangedCommand ??= new RelayCommand(_ =>
			{
				CurrentUser.Theme = CurrentUser.Theme == ApplicationTheme.Dark ? ApplicationTheme.Light : ApplicationTheme.Dark;
				ThemeManager.Current.ApplicationTheme = CurrentUser.Theme;
				DB.SaveChanges();
			});
		}
	}
}
