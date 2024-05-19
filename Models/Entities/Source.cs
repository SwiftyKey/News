using News.Models.Common;

namespace News.Models.Entities;

public class Source : BaseChangedEntity
{
	string? url;
	public string? Url
	{
		get => url;
		set
		{
			url = value;
			OnPropertyChanged(nameof(Url));
		}
	}
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? ImageUrl { get; set; }
	public List<Feed> Feeds { get; set; } = [];
}
