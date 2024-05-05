using News.Models.Common;

namespace News.Models.Entities;

public class FeedCategory : BaseUserCategory
{
	public IEnumerable<Feed>? Feeds { get; set; }
}
