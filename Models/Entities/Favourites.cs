namespace News.Models.Entities;

public class Favourites
{
	public int UserId { get; set; }
	public User? User { get; set; }

	public int FeedId { get; set; }
	public Feed? Feed { get; set; }
}

