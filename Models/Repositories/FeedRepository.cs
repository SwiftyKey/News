using News.Models.Entities;

namespace News.Models.Repositories;

public class FeedRepository : BaseRepository<Feed>
{
	public FeedRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Feeds;
	}

	public Feed? GetByUrl(string url) => set.FirstOrDefault(x => x.Link == url);
}
