using News.Models.Entities;

namespace News.Models.Repositories;

public class UserRepository : BaseRepository<User>
{
	public UserRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Users;
	}

	public User? GetByLogin(string login) => set.FirstOrDefault(x => x.Login == login);

	public void AddSourceByUserId(Source source, int userId) => GetById(userId).Sources.Add(source);

	public void AddFeedToFavouriteByUserId(Feed feed, int userId) => GetById(userId).FeedsFavourites.Add(feed);

	public void AddFeedToReadLaterByUserId(Feed feed, int userId) => GetById(userId).FeedsReadLater.Add(feed);
}
