using News.Models.Common;

namespace News.Models.Entities;

public class User : BaseChangedEntity
{
	string login = null!;
	public required string Login
	{
		get { return login; }
		set
		{ 
			login = value;
			OnPropertyChanged(nameof(Login));
		}
	}
	string hashPassword = null!;
	public required string HashPassword 
	{
		get { return hashPassword; }
		set
		{
			hashPassword = value;
			OnPropertyChanged(nameof(HashPassword));
		}
	}
	public IEnumerable<Source>? Sources { get; set; }
	public IEnumerable<FeedCategory>? FeedCategories { get; set; }
	public IEnumerable<SourceCategory>? SourceCategories { get; set; }
	public IEnumerable<Feed>? Favourites { get; set; }
	public IEnumerable<Feed>? FeedsReadLater { get; set; }
	public IEnumerable<Feed>? FeedsRead { get; set; }
}
