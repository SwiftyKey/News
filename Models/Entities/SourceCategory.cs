using News.Models.Common;

namespace News.Models.Entities;

public class SourceCategory : BaseUserCategory
{
	public IEnumerable<Source>? Sources { get; set; }
	public User User { get; set; } = null!;
}
