using News.Models.Common;
using System.Collections.ObjectModel;

namespace News.Models.Entities;

public class Publication : BaseEntity
{
	public required string Title { get; set; }
	public required string Link { get; set; }
	public DateTime? PublishingDate { get; set; }

	public ObservableCollection<User> UserFavourites { get; set; } = [];
	public ObservableCollection<Favourite> Favourites { get; set; } = [];

	public ObservableCollection<User> UserReadLater { get; set; } = [];
	public ObservableCollection<ReadLater> ReadLater { get; set; } = [];

	public Source Source { get; set; } = null!;
	public int SourceId { get; set; }
}
