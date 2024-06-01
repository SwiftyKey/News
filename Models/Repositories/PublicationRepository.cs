using News.Models.Entities;

namespace News.Models.Repositories;

public class PublicationRepository : BaseRepository<Publication>
{
	public PublicationRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Publications;
	}

	public Publication? GetByUrl(string url) => set.FirstOrDefault(x => x.Link == url);
}
