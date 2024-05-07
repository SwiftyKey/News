using News.Models.Entities;

namespace News.Models.Repositories;

public class SourceCategoryRepository : BaseRepository<SourceCategory>
{
	public SourceCategoryRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.SourceCategories;
	}

	public IEnumerable<SourceCategory> GetByUserId(int id)
	{
		return set.Where(c => c.Id == id);
	}
}