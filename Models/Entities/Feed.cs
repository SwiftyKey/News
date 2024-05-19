using News.Models.Common;

namespace News.Models.Entities;

public class Feed : BaseEntity
{
	public required string Title { get; set; }
	public required string Link { get; set; }
	public required string Description { get; set; }
	public DateTime? PublishingDate { get; set; }

	bool isFavourite = false;
	public bool IsFavourite
	{
		get => isFavourite;
		set
		{
			isFavourite = value;
			OnPropertyChanged(nameof(isFavourite));
		}
	}
	bool isReadLater = false;
	public bool IsReadLater
	{
		get => isReadLater;
		set
		{
			isReadLater = value;
			OnPropertyChanged(nameof(isReadLater));
		}
	}

	public Source Source { get; set; } = null!;
	public int SourceId { get; set; }
}
