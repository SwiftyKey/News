using Microsoft.EntityFrameworkCore;
using News.Models.Entities;
using News.Models.Repositories.Configs;

/**
	\brief Пространство имен, в котором содержатся классы, предназанченные для работы с базой данных
	\param Содержит классы:
		@ref AppContext
		@ref BaseRepository
		@ref FeedRepository
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
	public DbSet<Feed> Feeds { get; set; }

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
		modelBuilder.ApplyConfiguration(new FeedConfig());
		modelBuilder.ApplyConfiguration(new SourceConfig());
	}
}
