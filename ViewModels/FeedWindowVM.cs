using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Windows;

namespace News.ViewModels;

public class FeedWindowVM(Feed feed) : BaseViewModel
{
	public Feed CurrentFeed { get; set; } = feed;
	public bool IsFavourite { get; set; } = ApplicationVM.CurrentUser.FeedsFavourites.Any(f => f.Id == feed.Id);
	public bool IsAddToReadLater { get; set; } = ApplicationVM.CurrentUser.FeedsReadLater.Any(f => f.Id == feed.Id);

	private RelayCommand? addToFavouriteCommand;
	public RelayCommand? AddToFavouriteCommand
	{
		get
		{
			return addToFavouriteCommand ??= new RelayCommand(_ =>
			{
				_ = ApplicationVM.UserService.AddFeedToFavouriteByUserId(CurrentFeed, ApplicationVM.CurrentUser.Id);
			});
		}
	}

	private RelayCommand? addToReadLaterCommand;
	public RelayCommand? AddToReadLaterCommand
	{
		get
		{
			return addToReadLaterCommand ??= new RelayCommand(_ =>
			{
				_ = ApplicationVM.UserService.AddFeedToReadLaterByUserId(CurrentFeed, ApplicationVM.CurrentUser.Id);
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
