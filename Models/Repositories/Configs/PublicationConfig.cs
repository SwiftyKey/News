using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

/**
	\brief Пространство имен, в котором содержатся классы конфигурации для создания таблиц в базе данных
	\param Содержит классы:
		@ref PublicationConfig
		@ref SourceConfig
*/
namespace News.Models.Repositories.Configs;

/**
	\brief Класс, предназначенный для конфигурации создания таблицы Publications в базе данных
	
	Наследуется от IEntityTypeConfiguration
*/
public class PublicationConfig : IEntityTypeConfiguration<Publication>
{
	/**
		\brief Метод, конфигурирующий создание таблицы Publications
		\param[in] builder Билдер сущности Publication
	*/
	public void Configure(EntityTypeBuilder<Publication> builder)
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
			.WithMany(s => s.Publications)
			.HasForeignKey(f => f.SourceId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
