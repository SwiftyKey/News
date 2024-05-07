using News.Models.Common;

namespace News.Models.Entities;

public class Feed : BaseEntity
{
	public required string Title { get; set; }
	public required string Link { get; set; }
	public required string Description { get; set; }
	public DateTime? PublishingDate { get; set; }
	public string? ImareUrl { get; set; }
	public IEnumerable<User>? Users { get; set; }
	public IEnumerable<FeedCategory>? Categories { get; set; }
}
