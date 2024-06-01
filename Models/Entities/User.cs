using ModernWpf;
using News.Models.Common;
using News.Models.Hashers;
using System.Collections.ObjectModel;

namespace News.Models.Entities;

public class User : BaseEntity
{
	public required string Login { get; set; }

	private string hashPassword = null!;
	public required string HashPassword
	{
		get => hashPassword;
		set => hashPassword = SHA256Hasher.Hash(value);
	}

	public ObservableCollection<Source> Sources { get; set; } = [];

	public ObservableCollection<Publication> FavouritesPublications { get; set; } = [];
	public ObservableCollection<Favourite> Favourites { get; set; } = [];

	public ObservableCollection<Publication> ReadLaterPublications { get; set; } = [];
	public ObservableCollection<ReadLater> ReadLater { get; set; } = [];

	private bool notificationsOn = false;
	public bool NotificationsOn
	{
		get => notificationsOn;
		set
		{
			notificationsOn = value;
			OnPropertyChanged(nameof(NotificationsOn));
		}
	}

	private ApplicationTheme theme;
	public ApplicationTheme Theme
	{
		get => theme;
		set
		{
			theme = value;
			OnPropertyChanged(nameof(Theme));
		}
	}
}