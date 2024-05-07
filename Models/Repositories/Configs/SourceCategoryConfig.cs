using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

namespace News.Models.Repositories.Configs;

public class SourceCategoryConfig : IEntityTypeConfiguration<SourceCategory>
{
	public void Configure(EntityTypeBuilder<SourceCategory> builder)
	{
		builder
			.HasKey(sc => sc.Id);
		builder
			.HasIndex(sc => sc.Title)
			.IsUnique();
		builder
			.HasOne(sc => sc.User)
			.WithMany(u => u.SourceCategories);
		builder
			.HasMany(sc => sc.Sources)
			.WithMany(s => s.Categories);
	}
}
