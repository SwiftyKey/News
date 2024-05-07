using News.Models.Common;

namespace News.Models.Entities;

public class Source : BaseChangedEntity
{
	string url = null!;
	public required string Url
	{
		get { return url; }
		set
		{
			url = value;
			OnPropertyChanged();
		}
	}
	public IEnumerable<User> Users { get; set; } = null!;
	public IEnumerable<SourceCategory>? Categories { get; set; }
}
