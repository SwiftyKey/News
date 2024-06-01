using News.Models.Common;
using System.Collections.ObjectModel;

namespace News.Models.Entities;

public class Source : BaseEntity
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

	public ObservableCollection<Publication> Publications { get; set; } = [];
	public ObservableCollection<User> Users { get; set; } = [];
}
