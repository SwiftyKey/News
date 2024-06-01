using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Diagnostics;
using System.Windows;

namespace News.ViewModels;

/**
	\brief Модель представления для работы с окном отображения публикации
	\param Publication Отображаемая публикация

	Наследуется от BaseChanged
*/
public class PublicationWindowVM(Publication Publication) : BaseChanged
{
	/// Отображаемая публикация
	public Publication CurrentPublication { get; set; } = Publication;

	public bool IsFavourite{ get; set; } = ApplicationVM.CurrentUser.FavouritesPublications.Contains(Publication);

	public bool IsReadLater { get; set; } = ApplicationVM.CurrentUser.ReadLaterPublications.Contains(Publication);

	/// Команда добавления публикации в избранное
	private RelayCommand? favouriteCommand;
	/// Свойство для работы с favouriteCommand
	public RelayCommand? FavouriteCommand
	{
		get
		{
			return favouriteCommand ??= new RelayCommand((state) =>
			{
				if (state is null) return;

				if ((bool)state)
					ApplicationVM.CurrentUser?.FavouritesPublications.Add(CurrentPublication);
				else
					ApplicationVM.CurrentUser?.FavouritesPublications.Remove(CurrentPublication);

				ApplicationVM.DB.SaveChanges();
			});
		}
	}

	/// Команда добавления публикации в отложенное
	private RelayCommand? readLaterCommand;
	/// Свойство для работы с readLaterCommand
	public RelayCommand? ReadLaterCommand
	{
		get
		{
			return readLaterCommand ??= new RelayCommand((state) =>
			{
				if (state is null) return;

				if ((bool)state)
					ApplicationVM.CurrentUser?.ReadLaterPublications.Add(CurrentPublication);
				else
					ApplicationVM.CurrentUser?.ReadLaterPublications.Remove(CurrentPublication);

				ApplicationVM.DB.SaveChanges();
			});
		}
	}

	/// Команда копирования ссылки на публикацию
	private RelayCommand? copyLinkCommand;
	/// Свойство для работы с copyLinkCommand
	public RelayCommand? CopyLinkCommand
	{
		get
		{
			return copyLinkCommand ??= new RelayCommand(_ =>
			{
				Clipboard.SetText(CurrentPublication.Link);
			});
		}
	}

	/// Команда открытие ссылки в браузере по умолчанию
	private RelayCommand? openLinkCommand;
	/// Свойство для работы с openLinkCommand
	public RelayCommand? OpenLinkCommand
	{
		get
		{
			return openLinkCommand ??= new RelayCommand(_ =>
			{
				Process.Start(new ProcessStartInfo(CurrentPublication.Link) { UseShellExecute = true });
			});
		}
	}
}
