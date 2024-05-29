using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Diagnostics;
using System.Windows;

namespace News.ViewModels;

/**
	\brief Модель представления для работы с окном отображения публикации
	\param feed Отображаемая публикация

	Наследуется от BaseChanged
*/
public class FeedWindowVM(Feed feed) : BaseChanged
{
	/// Отображаемая публикация
	public Feed CurrentFeed { get; set; } = feed;

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
				CurrentFeed.IsFavourite = (bool)state;
				_ = ApplicationVM.FeedService.UpdateAsync(CurrentFeed);
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
				CurrentFeed.IsReadLater = (bool)state;
				_ = ApplicationVM.FeedService.UpdateAsync(CurrentFeed);
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
				Clipboard.SetText(CurrentFeed.Link);
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
				Process.Start(new ProcessStartInfo(CurrentFeed.Link) { UseShellExecute = true });
			});
		}
	}
}
