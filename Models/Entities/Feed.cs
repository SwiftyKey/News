using News.Models.Common;

namespace News.Models.Entities;

public class Feed : BaseEntity
{
	public required string Title { get; set; }
	public required string Link { get; set; }
	public required string Description { get; set; }
	public DateTime? PublishingDate { get; set; }
	public List<User> UsersFavourites { get; set; } = [];
	public List<User> UsersReadLater { get; set; } = [];
	public Source Source { get; set; } = null!;
	public int SourceId { get; set; }
}
