using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Diagnostics;
using System.Windows;

namespace News.ViewModels;

public class PublicationWindowVM(Publication publication) : BaseChanged
{
	public Publication CurrentPublication { get; set; } = publication;
	public bool IsFavourite{ get; set; } = ApplicationVM.CurrentUser.FavouritesPublications.Contains(publication);
	public bool IsReadLater { get; set; } = ApplicationVM.CurrentUser.ReadLaterPublications.Contains(publication);

	private RelayCommand? favouriteCommand;
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

	private RelayCommand? readLaterCommand;
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

	private RelayCommand? copyLinkCommand;
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

	private RelayCommand? openLinkCommand;
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
