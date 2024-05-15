using Microsoft.EntityFrameworkCore;
using News.Models.Entities;
using News.Models.Repositories.Configs;

namespace News.Models.Repositories;

public class AppContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Source> Sources { get; set; }
	public DbSet<Feed> Feeds { get; set; }

	public AppContext()
	{
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=LocalUsersDb.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserConfig());
		modelBuilder.ApplyConfiguration(new FeedConfig());
		modelBuilder.ApplyConfiguration(new SourceConfig());
	}
}
