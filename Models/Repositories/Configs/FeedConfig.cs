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
