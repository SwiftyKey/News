using News.Models.Common;

namespace News.Models.Entities;

public class Source : BaseChangedEntity
{
	string url;
	public required string Url
	{
		get { return url; }
		set
		{
			url = value;
			OnPropertyChanged();
		}
	}
}
