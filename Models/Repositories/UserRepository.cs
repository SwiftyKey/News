using News.Models.Entities;

namespace News.Models.Repositories;

public class UserRepository : BaseRepository<User>
{
	public UserRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Users;
	}

	public User? GetByLogin(string login) => set.FirstOrDefault(x => x.Login == login);

	public void AddSourceByUser(Source source, User user) => user.Sources.Add(source);

	public void AddPublicationToFavouriteByUserId(Publication publication, int userId) => GetById(userId).FavouritesPublications.Add(publication);

	public void AddPublicationToReadLaterByUserId(Publication publication, int userId) => GetById(userId).ReadLaterPublications.Add(publication);

	public void DeletePublicationFromFavouritesByUserId(Publication publication, int userId) => GetById(userId).FavouritesPublications.Remove(publication);

	public void DeletePublicationFromReadLaterByUserId(Publication publication, int userId) => GetById(userId).ReadLaterPublications.Remove(publication);
}