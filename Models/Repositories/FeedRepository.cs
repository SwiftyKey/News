using News.Models.Entities;

namespace News.Models.Repositories;

public class FeedRepository : BaseRepository<Feed>
{
	public FeedRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Feeds;
	}
}
