using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

namespace News.Models.Repositories.Configs;

/**
	\brief Класс, предназначенный для конфигурации создания таблицы Sources в базе данных
	
	Наследуется от IEntityTypeConfiguration
*/
public class SourceConfig : IEntityTypeConfiguration<Source>
{
	/**
		\brief Метод, конфигурирующий создание таблицы Sources
		\param[in] builder Билдер сущности Source
	*/
	public void Configure(EntityTypeBuilder<Source> builder)
	{
		builder
			.HasKey(s => s.Id);
		builder
			.HasIndex(s => s.Url)
			.IsUnique();
		builder
			.HasIndex(s => s.Url)
			.IsUnique();
		builder
			.HasIndex(s => s.Title)
			.IsUnique();
		builder
			.Property(s => s.Description);
		builder
			.Property(s => s.ImageUrl);
		builder
			.HasMany(s => s.Feeds)
			.WithOne(f => f.Source)
			.HasForeignKey(f => f.SourceId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
