using News.Models.Entities;

namespace News.Models.Repositories;

public class UserRepository : BaseRepository<User>
{
	public UserRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Users;
	}

	public User? GetByLogin(string login) => set.FirstOrDefault(x => x.Login == login);
}
