using Microsoft.Extensions.DependencyInjection;

namespace News.Models.Repositories.Extensions;

public static class RepositoriesExtension
{
	public static void AddRepositories(this IServiceCollection collection)
	{
		collection.AddScoped<SourceRepository>();
		collection.AddScoped<FeedRepository>();
		collection.AddScoped<UserRepository>();
	}
}