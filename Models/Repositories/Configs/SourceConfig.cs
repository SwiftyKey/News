using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

namespace News.Models.Repositories.Configs;

public class SourceConfig : IEntityTypeConfiguration<Source>
{
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
			.HasMany(s => s.Users)
			.WithMany(u => u.Sources);
		builder
			.HasMany(s => s.Feeds)
			.WithOne(f => f.Source);
	}
}
