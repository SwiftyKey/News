using News.Models.Common;

namespace News.Models.Entities;

public class User : BaseChangedEntity
{
	string login;
	public required string Login
	{
		get { return login; }
		set
		{ 
			login = value;
			OnPropertyChanged(nameof(Login));
		}
	}
	string hashPassword;
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
	public IEnumerable<Favourite>? Favourites { get; set; }
	public IEnumerable<FeedCategory>? UserCategories { get; set; }
	public IEnumerable<Feed>? ReadLater {  get; set; }
}
