using News.Models.Common;

namespace News.Models.Entities;

public class Source : BaseEntity
{
	public string Url { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string? ImageUrl { get; set; }
	public List<User> Users { get; set; } = [];
	public List<Feed> Feeds { get; set; } = [];
}
