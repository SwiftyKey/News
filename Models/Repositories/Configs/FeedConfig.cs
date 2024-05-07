using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

namespace News.Models.Repositories.Configs;

public class FeedConfig : IEntityTypeConfiguration<Feed>
{
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
			.Property(f => f.Description)
			.IsRequired();
		builder
			.Property(f => f.PublishingDate);
		builder
			.Property(f => f.ImareUrl);
		builder
			.HasMany(f => f.Users)
			.WithMany(u => u.FeedsRead);
		builder
			.HasMany(f => f.Users)
			.WithMany(u => u.FeedsReadLater);
		builder
			.HasMany(f => f.Users)
			.WithMany(u => u.Favourites);
		builder
			.HasMany(f => f.Categories)
			.WithMany(fc => fc.Feeds);
	}
}
