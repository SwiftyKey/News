using News.Models.Entities;

namespace News.Models.Repositories;

public class SourceRepository : BaseRepository<Source>
{
	public SourceRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Sources;
	}
}
