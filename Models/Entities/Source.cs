using News.Models.Common;

namespace News.Models.Entities;

public class Source : BaseEntity
{
	public string Url { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public IEnumerable<User> Users { get; set; } = null!;
	public IEnumerable<Feed> Feeds { get; set; } = null!;
}
