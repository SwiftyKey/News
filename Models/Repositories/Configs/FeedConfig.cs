using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

/**
	\brief Пространство имен, в котором содержатся классы конфигурации для создания таблиц в базе данных
	\param Содержит классы:
		@ref FeedConfig
		@ref SourceConfig
*/
namespace News.Models.Repositories.Configs;

/**
	\brief Класс, предназначенный для конфигурации создания таблицы Feeds в базе данных
	
	Наследуется от IEntityTypeConfiguration
*/
public class FeedConfig : IEntityTypeConfiguration<Feed>
{
	/**
		\brief Метод, конфигурирующий создание таблицы Feeds
		\param[in] builder Билдер сущности Feed
	*/
	public void Configure(EntityTypeBuilder<Feed> builder)
	{
		builder
			.HasKey(f => f.Id);
		builder
			.Property(f => f.Title)
			.IsRequired();
		builder
			.Property(f => f.Link)
			.IsRequired();
		builder
			.Property(f => f.PublishingDate);
		builder
			.Property(f => f.IsFavourite);
		builder
			.Property(f => f.IsReadLater);
		builder
			.HasOne(f => f.Source)
			.WithMany(s => s.Feeds)
			.HasForeignKey(f => f.SourceId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
