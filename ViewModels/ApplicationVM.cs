using CodeHollow.FeedReader;
using Microsoft.EntityFrameworkCore;
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
	public static PublicationService PublicationService { get; set; } = new (new PublicationRepository(DB));
	/// Сервис работы с таблицей Sources
	public static SourceService SourceService { get; set; } = new (new SourceRepository(DB));

	/// Коллекция источников
	public static ObservableCollection<Source> Sources { get; set; } = [];
	/// Коллекция публикаций
	public static ObservableCollection<Publication> Publications { get; set; } = [];
	public static ObservableCollection<Publication> ReadLaterList { get; set; } = [];
	public static ObservableCollection<Publication> FavouriteList { get; set; } = [];

	/// Наблюдатель, оповещающий о выходе новых публикаций
	public static Observer Observer { get; set; } = new();

	/// Конструктор класса ApplicationVM
	public ApplicationVM()
	{
		DB.Publications.Load();
		DB.Sources.Load();

		Publications = DB.Publications.Local.ToObservableCollection();
		Sources = DB.Sources.Local.ToObservableCollection();

		ReadLaterList = new ObservableCollection<Publication>
		(
			Publications
			.Where(p => p.IsReadLater ==  true)
			.ToList()
		);

		FavouriteList = new ObservableCollection<Publication>
		(
			Publications
			.Where(p => p.IsFavourite == true)
			.ToList()
		);
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

						if (Sources.Any(s => s.Url == sourceUrl)) return;

						var result = FeedReader.ReadAsync(sourceUrl).Result;

						var source = new Source
						{
							Url = sourceUrl,
							Title = result.Title,
							Description = result.Description,
							ImageUrl = result.ImageUrl
						};

						_ = SourceService.AddAsync(source);

						_ = PublicationService.AddRangeAsyncBySource(source);
					}
					catch (XmlException ex)
					{
						MessageBox.Show("Данная RSS-ссылка не поддерживается");
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
}
