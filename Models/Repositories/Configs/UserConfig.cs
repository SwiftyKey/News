using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Models.Entities;

namespace News.Models.Repositories.Configs;

public class UserConfig : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.HasKey(u => u.Id);
		builder
			.HasIndex(u => u.Login)
			.IsUnique();
		builder
			.HasIndex(u => u.HashPassword)
			.IsUnique();
		builder
			.HasMany(u => u.FeedsFavourites)
			.WithMany(f => f.UsersFavourites);
		builder
			.HasMany(u => u.FeedsReadLater)
			.WithMany(f => f.UsersReadLater);
	}
}
