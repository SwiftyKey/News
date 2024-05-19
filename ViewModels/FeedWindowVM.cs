using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Windows;

namespace News.ViewModels;

public class FeedWindowVM : BaseViewModel
{
	public Feed CurrentFeed { get; set; }

	private bool isFavourite;
	public bool IsFavourite
	{
		get => isFavourite;
		set
		{
			isFavourite = value;
			OnPropertyChanged(nameof(IsFavourite));
		}
	}

	private bool isAddToReadLater;
	public bool IsAddToReadLater
	{
		get => isAddToReadLater;
		set
		{
			isAddToReadLater = value;
			OnPropertyChanged(nameof(IsAddToReadLater));
		}
	}

	public FeedWindowVM(Feed feed)
	{
		CurrentFeed = feed;

		IsFavourite = ApplicationVM.CurrentUser.FeedsFavourites.Any(f => f.Id == feed.Id);
		IsAddToReadLater = ApplicationVM.CurrentUser.FeedsReadLater.Any(f => f.Id == feed.Id);
	}

	private RelayCommand? favouriteCommand;
	public RelayCommand? FavouriteCommand
	{
		get
		{
			return favouriteCommand ??= new RelayCommand((state) =>
			{
				if (state is null) return;
				if ((bool)state)
					_ = ApplicationVM.UserService.AddFeedToFavouriteByUserIdAsync(CurrentFeed, ApplicationVM.CurrentUser.Id);
				else
					_ = ApplicationVM.UserService.DeleteFeedFromFavouritesByUserIdAsync(CurrentFeed, ApplicationVM.CurrentUser.Id);
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
				if ((bool)state)
					_ = ApplicationVM.UserService.AddFeedToReadLaterByUserIdAsync(CurrentFeed, ApplicationVM.CurrentUser.Id);
				else
					_ = ApplicationVM.UserService.DeleteFeedFromReadLaterByUserIdAsync(CurrentFeed, ApplicationVM.CurrentUser.Id);
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
}
