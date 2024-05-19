using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Diagnostics;
using System.Windows;

namespace News.ViewModels;

public class FeedWindowVM(Feed feed) : BaseViewModel
{
	public Feed CurrentFeed { get; set; } = feed;

	private RelayCommand? favouriteCommand;
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

	private RelayCommand? readLaterCommand;
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

	private RelayCommand? copyLinkCommand;
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

	private RelayCommand? openLinkCommand;
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
