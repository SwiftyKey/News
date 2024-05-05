namespace News.Models.Common;

public abstract class BaseUserCategory : BaseChangedEntity
{
	string? title;
	public string? Title
	{
		get { return title; }
		set
		{
			title = value;
			OnPropertyChanged();
		}
	}
}
