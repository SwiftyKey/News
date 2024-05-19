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
		//Database.EnsureDeleted();
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

		modelBuilder
			.Entity<Feed>()
			.HasMany(f => f.UsersFavourites)
			.WithMany(u => u.FeedsFavourites)
			.UsingEntity<Favourites>
			(
				j => j
					.HasOne(fj => fj.User)
					.WithMany(u => u.Favourites)
					.HasForeignKey(fj => fj.UserId),
				j => j
					.HasOne(fj => fj.Feed)
					.WithMany(f => f.Favourites)
					.HasForeignKey(fj => fj.FeedId),
				j =>
				{
					j.HasKey(t => new { t.UserId, t.FeedId });
					j.ToTable("Favourites");
				}
			);

		modelBuilder
			.Entity<Feed>()
			.HasMany(f => f.UsersReadLater)
			.WithMany(u => u.FeedsReadLater)
			.UsingEntity<ReadLater>
			(
				j => j
					.HasOne(fj => fj.User)
					.WithMany(u => u.ReadLater)
					.HasForeignKey(fj => fj.UserId),
				j => j
					.HasOne(fj => fj.Feed)
					.WithMany(f => f.ReadLater)
					.HasForeignKey(fj => fj.FeedId),
				j =>
				{
					j.HasKey(t => new { t.UserId, t.FeedId });
					j.ToTable("ReadLater");
				}
			);
	}
}
