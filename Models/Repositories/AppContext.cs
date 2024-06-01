using Microsoft.EntityFrameworkCore;
using News.Models.Entities;
using News.Models.Repositories.Configs;

/**
	\brief Пространство имен, в котором содержатся классы, предназанченные для работы с базой данных
	\param Содержит классы:
		@ref AppContext
		@ref BaseRepository
		@ref PublicationRepository
		@ref SourceRepository
*/
namespace News.Models.Repositories;

/**
	\brief Класс, создающий контекст базы данных
	
	Наследуется от DbContext
*/
public class AppContext : DbContext
{
	/// Таблица источников
	public DbSet<Source> Sources { get; set; }
	/// Таблица публикаций
	public DbSet<Publication> Publications { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Favourite> Favourites { get; set; }
	public DbSet<ReadLater> ReadLater { get; set; }

	/// Конструктор класса AppContext
	public AppContext()
	{
		//Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	/**
		\brief Переопределенный метод, для подключения к базе данных
		\param[in] optionsBuilder Билдер контекста базы данных
	*/
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=LocalUsersDb.db");
	}

	/**
		\brief Переопределенный метод, для конфигурации таблиц
		\param[in] modelBuilder Билдер таблиц базы данных
	*/
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
