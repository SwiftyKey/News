using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
			.HasMany(f => f.UsersFavourites)
			.WithMany(u => u.FeedsFavourites);
		builder
			.HasMany(f => f.UsersReadLater)
			.WithMany(u => u.FeedsReadLater);
		builder
			.HasOne(f => f.Source)
			.WithMany(s => s.Feeds)
			.HasForeignKey(f => f.SourceId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
