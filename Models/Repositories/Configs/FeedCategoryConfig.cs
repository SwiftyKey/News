using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

namespace News.Models.Repositories.Configs;

public class FeedCategoryConfig : IEntityTypeConfiguration<FeedCategory>
{
	public void Configure(EntityTypeBuilder<FeedCategory> builder)
	{
		builder
			.HasKey(f => f.Id);
		builder
			.HasIndex(f => f.Title)
			.IsUnique();
		builder
			.HasOne(fc => fc.User)
			.WithMany(u => u.FeedCategories);
		builder
			.HasMany(fc => fc.Feeds)
			.WithMany(f => f.Categories);
	}
}
