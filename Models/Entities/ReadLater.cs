namespace News.Models.Entities;

public class ReadLater
{
	public int UserId { get; set; }
	public User? User { get; set; }

	public int FeedId { get; set; }
	public Feed? Feed { get; set; }
}
