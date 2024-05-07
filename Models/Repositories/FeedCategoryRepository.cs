using News.Models.Entities;

namespace News.Models.Repositories;

public class FeedCategoryRepository : BaseRepository<FeedCategory>
{
	public FeedCategoryRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.FeedCategories;
	}

	public IEnumerable<FeedCategory> GetByUserId(int id)
	{
		return set.Where(c => c.Id == id);
	}
}