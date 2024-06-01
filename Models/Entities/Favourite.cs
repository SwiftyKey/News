namespace News.Models.Entities;

public class Favourite
{
	public int UserId { get; set; }
	public User? User { get; set; }

	public int PublicationId { get; set; }
	public Publication? Publication { get; set; }
}