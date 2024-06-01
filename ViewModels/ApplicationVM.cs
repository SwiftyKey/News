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
using System.ComponentModel;
using System.Windows;
using System.Xml;

/**
	\brief Пространство имен, в котором содержатся модели представления (ViewModel)
	\param Содержит классы:
		@ref ApplicationVM
		@ref PublicationWindowVM
		@ref SettingsVM
*/
namespace News.ViewModels;

/**
	\brief Основной класс для взаимодействия графического интерфейса и моделей
	
	Наследуется от BaseChanged
*/
public class ApplicationVM : BaseChanged
{
	/// Контекст базы данных
	public static Models.Repositories.AppContext DB { get; set; } = new();
	/// Сервис работы с таблицей Publications
	public static PublicationService PublicationService { get; set; } = new(new PublicationRepository(DB));
	/// Сервис работы с таблицей Sources
	public static SourceService SourceService { get; set; } = new(new SourceRepository(DB));
	public static UserService UserService { get; set; } = new(new UserRepository(DB));

	/// Коллекция публикаций
	private static ObservableCollection<Publication> publications = [];
	public static ObservableCollection<Publication> Publications
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

	/// Наблюдатель, оповещающий о выходе новых публикаций
	public static Observer Observer { get; set; } = new();

	/// Конструктор класса ApplicationVM
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

	/// Команда добавления источника
	private RelayCommand? addSourceCommand;
	/// Свойство для работы с addSourceCommand
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

	/// Команда удаления источника
	private RelayCommand? removeSourceCommand;
	/// Свойство для работы с removeSourceCommand
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

	/// Команда показа выбранной публикации
	private static RelayCommand? viewPublicationCommand;
	/// Свойство для работы с viewPublicationCommand
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
