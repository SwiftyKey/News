using Microsoft.EntityFrameworkCore;
using News.Models.Entities;
using News.Models.Repositories.Configs;

namespace News.Models.Repositories;

public class AppContext : DbContext
{
	public DbSet<Source> Sources { get; set; }
	public DbSet<Publication> Publications { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Favourite> Favourites { get; set; }
	public DbSet<ReadLater> ReadLater { get; set; }

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
		modelBuilder.ApplyConfiguration(new PublicationConfig());
		modelBuilder.ApplyConfiguration(new SourceConfig());
		modelBuilder.ApplyConfiguration(new UserConfig());

		modelBuilder
			.Entity<Publication>()
			.HasMany(f => f.UserFavourites)
			.WithMany(u => u.FavouritesPublications)
			.UsingEntity<Favourite>
			(
				j => j
					.HasOne(fj => fj.User)
					.WithMany(u => u.Favourites)
					.HasForeignKey(fj => fj.UserId),
				j => j
					.HasOne(fj => fj.Publication)
					.WithMany(f => f.Favourites)
					.HasForeignKey(fj => fj.PublicationId),
				j =>
				{
					j.HasKey(t => new { t.UserId, t.PublicationId });
					j.ToTable("Favourites");
				}
			);

		modelBuilder
			.Entity<Publication>()
			.HasMany(f => f.UserReadLater)
			.WithMany(u => u.ReadLaterPublications)
			.UsingEntity<ReadLater>
			(
				j => j
					.HasOne(fj => fj.User)
					.WithMany(u => u.ReadLater)
					.HasForeignKey(fj => fj.UserId),
				j => j
					.HasOne(fj => fj.Publication)
					.WithMany(f => f.ReadLater)
					.HasForeignKey(fj => fj.PublicationId),
				j =>
				{
					j.HasKey(t => new { t.UserId, t.PublicationId });
					j.ToTable("ReadLater");
				}
			);
	}
}
