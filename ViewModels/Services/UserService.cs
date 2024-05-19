using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

public class UserService(UserRepository userRepository)
{
	public async Task AddAsync(User user)
	{
		await userRepository.AddAsync(user);
		await userRepository.SaveChangesAsync();
	}

	public async Task DeleteAsync(User user)
	{
		userRepository.Delete(user);
		await userRepository.SaveChangesAsync();
	}

	public async Task AddSourceByUserIdAsync(Source source, int userId)
	{
		userRepository.AddSourceByUserId(source, userId);
		await userRepository.SaveChangesAsync();
	}

	public async Task AddFeedToFavouriteByUserIdAsync(Feed feed, int userId)
	{
		userRepository.AddFeedToFavouriteByUserId(feed, userId);
		await userRepository.SaveChangesAsync();
	}

	public async Task AddFeedToReadLaterByUserIdAsync(Feed feed, int userId)
	{
		userRepository.AddFeedToReadLaterByUserId(feed, userId);
		await userRepository.SaveChangesAsync();
	}

	public async Task DeleteFeedFromFavouritesByUserIdAsync(Feed feed, int userId)
	{
		userRepository.DeleteFeedFromFavouritesByUserId(feed, userId);
		await userRepository.SaveChangesAsync();
	}

	public async Task DeleteFeedFromReadLaterByUserIdAsync(Feed feed, int userId)
	{
		userRepository.DeleteFeedFromReadLaterByUserId(feed, userId);
		await userRepository.SaveChangesAsync();
	}

	public IEnumerable<User> GetAll()
	{
		return userRepository.GetAll();
	}

	public User GetById(int id)
	{
		return userRepository.GetById(id);
	}

	public User? GetByLogin(string login)
	{
		return userRepository.GetByLogin(login);
	}

	public async Task UpdateAsync(User user)
	{
		var entity = userRepository.GetById(user.Id);

		userRepository.Update(entity);
		await userRepository.SaveChangesAsync();
	}
}
